using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Infrastructure.CommandProcessor;
using Kangaroo.Models;
using MongoDB.Driver;
using Kangaroo.Infrastructure.Commands;

namespace Kangaroo.Infrastructure.Commands
{
    public class CreateOrUpdateTimeSheetHandler : ICommandHandler<CreateOrUpdateTimeSheet>
    {
        private readonly ISession session;

        public CreateOrUpdateTimeSheetHandler(ISession session)
        {
            this.session = session;
        }
        public void Execute(CreateOrUpdateTimeSheet param)
        {
            List<TimeEntry> create = new List<TimeEntry>();
            List<TimeEntry> update = new List<TimeEntry>();

            foreach (var item in param.TimeEntries)
            {
                item.UserName = param.UserName;
                if (session.GetQueryable<TimeEntry>().Any(t => t.Id == item.Id))
                {
                    update.Add(item);
                }
                else
                {
                    create.Add(item);
                }
            }
            create = create.Where(te => te.Quantity != 0 && te.Quantity != null).ToList();

            if (create.Count > 0)
                session.AddBatch(create);
            if (update.Count > 0)
                session.UpdateBatch(update);
        }
    }
}