using CodingExercise.Models;
using CodingExercise.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace CodingExercise.Utilities
{
    /// <summary>
    /// A class that initiates the export of mongo DB data into SQL Server
    /// </summary>
    public sealed class DevExporter : IDevExporter
    {
        private IMdbRepository<SourceDeveloper> _repository = null;
        private ISqlDbRepository _sqlDest = null;

        /// <summary>
        /// Initialize the repositories used
        /// </summary>
        /// <param name="mdbRepository"></param>
        public DevExporter(IMdbRepository<SourceDeveloper> mdbRepository, ISqlDbRepository sqlDest)
        {
            _repository = mdbRepository;
            _sqlDest = sqlDest;
        }

        /// <summary>
        /// The main method which initiate the process
        /// </summary>
        /// <returns></returns>
        public ReportBatch Export()
        {
            var sourceDevs = _repository.Find(d => d.Processed == null);

            if (sourceDevs == null) throw new ApplicationException("No data to import.");

            var totalDocRead = sourceDevs.Count();
            var totalDevs = 0;
            var totalAwards = 0;
            var totalContribs = 0;

            foreach (var sourceDev in sourceDevs)
            {
                var name = new Name
                {
                    FirstName = sourceDev.Name.FirstName,
                    LastName = sourceDev.Name.LastName
                };

                var dev = new Developer
                {
                    RootID = sourceDev.ID,
                    AKA = sourceDev.Name.AKA,
                    Birth = sourceDev.Birth,
                    Death = sourceDev.Death,
                    DevName = name,
                    Title = sourceDev.Title,
                    Awards = AddAwards(sourceDev, name),
                    Contribs = AddContribs(sourceDev, name)
                };

                if (SaveData(dev, sourceDev))
                {
                    totalDevs += 1;
                    totalAwards += dev.Awards.Count;
                    totalContribs += dev.Contribs.Count;
                }
            }

            ReportBatch rb = new ReportBatch
            {
                RunDate = DateTime.Now,
                TotalAwardWritten = totalAwards,
                TotalContribsWritten = totalContribs,
                TotalDeveloperWritten = totalDevs,
                TotalDocumentRead = totalDocRead,
                User = this.CurrentUser
            };

            //Save the summary of the report batch in db
            SaveReportBatch(rb);

            return rb;
        }

        /// <summary>
        /// Saves the data to the SQL Server for each row in mongo db and set the record in 
        /// mongo db to processed
        /// </summary>
        /// <param name="dev">Developer</param>
        /// <param name="sourceDev">SourceDeveloper</param>
        /// <returns>Boolean. True if saved successfully, otherwise it's false</returns>
        private bool SaveData(Developer dev, SourceDeveloper sourceDev)
        {
            bool succcess = false;

            try
            {
                //Use transaction scope to ensure that both update in SQL and Mongo are successful
                using (var ts = new TransactionScope())
                {
                    _sqlDest.AddDeveloper(dev);
                    _sqlDest.SaveChanges();

                    //Now we mark the record in source to processed
                    this.SetDocumentAsProcessed(sourceDev);

                    ts.Complete();
                    succcess = true;
                }
            }
            catch (Exception)
            {
                //Log the error here in case save failed, we don't rethrow it since we need to continue
                //processing the other records in the list
            }
            return succcess;
        }

        /// <summary>
        /// Sve the summary of the report batch
        /// </summary>
        /// <param name="rb"></param>
        private void SaveReportBatch(ReportBatch rb)
        {
            try
            {
                _sqlDest.AddReportBatch(rb);
                _sqlDest.SaveChanges();
            }
            catch (Exception)
            {
                //log the error
            }
        }

        /// <summary>
        /// Tag the record as processed in MongoDB
        /// </summary>
        /// <param name="sDev"></param>
        private void SetDocumentAsProcessed(SourceDeveloper sDev)
        {
            _repository.Update(sDev);
        }

        /// <summary>
        /// Constructs a List of Award per Root document
        /// </summary>
        /// <param name="sourceDev">SourceDeveloper </param>
        /// <param name="name">Instance of the Name class</param>
        /// <returns> List<Award> </returns>
        private List<Award> AddAwards(SourceDeveloper sourceDev, Name name)
        {
            var hsAwards = new List<Award>();
            var awards = sourceDev.Awards;

            if (awards != null && awards.Count > 0)
            {
                foreach (var award in awards)
                {
                    hsAwards.Add(new Award
                    {
                        RootID = sourceDev.ID,
                        AwardBy = award.AwardBy,
                        AwardYear = award.AwardYear,
                        AwardName = award.AwardName,
                        DevName = name,
                        Birth = sourceDev.Birth
                    });
                }
            }

            return hsAwards;
        }

        /// <summary>
        /// Constructs a List of Contrib per Root document
        /// </summary>
        /// <param name="sourceDev">SourceDeveloper </param>
        /// <param name="name">Instance of the Name class</param>
        /// <returns> List<Contrib> </returns>
        private List<Contrib> AddContribs(SourceDeveloper sourceDev, Name name)
        {
            var hsContribs = new List<Contrib>();
            var contribs = sourceDev.Contribs;

            if (contribs != null && contribs.Count > 0)
            {
                foreach (string contrib in contribs)
                {
                    hsContribs.Add(new Contrib
                    {
                        RootID = sourceDev.ID,
                        Name = contrib,
                        Birth = sourceDev.Birth,
                        DevName = name
                    });
                }
            }

            return hsContribs;
        }

        /// <summary>
        /// Returns the current user
        /// </summary>
        private string CurrentUser
        {
            get
            {
                string user =HttpContext.Current.User.Identity.Name;

                if (!string.IsNullOrEmpty(user))
                {
                    var slashPos = user.IndexOf(@"\");
                    slashPos = slashPos == -1 ? 1 : slashPos + 1;

                    return user.Substring(slashPos).Trim();
                }
                else return string.Empty;
            }
        }
        
    }
}