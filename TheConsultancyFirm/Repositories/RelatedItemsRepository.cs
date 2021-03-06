﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheConsultancyFirm.Common;
using TheConsultancyFirm.Data;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.Repositories
{
    public class RelatedItemsRepository : IRelatedItemsRepository
    {
        private const string RelatedItemQuery =
@"WITH MyTags AS (
    SELECT TagId FROM {0}Tag WHERE {0}Id = @id
)
SELECT TOP(3) type, Id, Title, PhotoPath, t.CommonTags / (CASE WHEN t.TotalTags = 0 THEN 1 ELSE t.TotalTags END) AS score FROM (
  SELECT
    'Case' AS type,
    Cases.Id,
    Cases.Title,
    Cases.PhotoPath,
    COUNT(MyTags.TagId) + 0.0 AS CommonTags,
    (SELECT COUNT(*) FROM (SELECT TagId FROM CaseTag WHERE CaseTag.CaseId = Cases.Id UNION SELECT TagId FROM MyTags) t) AS TotalTags
  FROM Cases
  LEFT JOIN CaseTag ON Cases.Id = CaseTag.CaseId
  LEFT JOIN MyTags ON MyTags.TagId = CaseTag.TagId
  WHERE Cases.Deleted = 0 AND Cases.Enabled = 1 AND Cases.Language = @language
  GROUP BY Cases.Id, Cases.Title, Cases.PhotoPath

  UNION

  SELECT
    'NewsItem',
    NewsItems.Id,
    NewsItems.Title,
    NewsItems.PhotoPath,
    COUNT(MyTags.TagId) + 0.0,
    (SELECT COUNT(*) FROM (SELECT TagId FROM NewsItemTag WHERE NewsItemTag.NewsItemId = NewsItems.Id UNION SELECT TagId FROM MyTags) t)
  FROM NewsItems
  LEFT JOIN NewsItemTag ON NewsItems.Id = NewsItemTag.NewsItemId
  LEFT JOIN MyTags ON MyTags.TagId = NewsItemTag.TagId
  WHERE NewsItems.Deleted = 0 AND NewsItems.Enabled = 1 AND NewsItems.Language = @language
  GROUP BY NewsItems.Id, NewsItems.Title, NewsItems.PhotoPath

  UNION

  SELECT
    'Download',
    Downloads.Id,
    Downloads.Title,
    NULL, -- PhotoPath
    COUNT(MyTags.TagId) + 0.0,
    (SELECT COUNT(*) FROM (SELECT TagId FROM DownloadTag WHERE DownloadTag.DownloadId = Downloads.Id UNION SELECT TagId FROM MyTags) t)
  FROM Downloads
  LEFT JOIN DownloadTag ON Downloads.Id = DownloadTag.DownloadId
  LEFT JOIN MyTags ON MyTags.TagId = DownloadTag.TagId
  WHERE Downloads.Deleted = 0 AND Downloads.Enabled = 1 AND Downloads.Language = @language
  GROUP BY Downloads.Id, Downloads.Title

  UNION

  SELECT
    'Solution',
    Solutions.Id,
    Solutions.Title,
    NULL, -- PhotoPath
    COUNT(MyTags.TagId) + 0.0,
    (SELECT COUNT(*) FROM (SELECT TagId FROM SolutionTag WHERE SolutionTag.SolutionId = Solutions.Id UNION SELECT TagId FROM MyTags) t)
  FROM Solutions
  LEFT JOIN SolutionTag ON Solutions.Id = SolutionTag.SolutionId
  LEFT JOIN MyTags ON MyTags.TagId = SolutionTag.TagId
  WHERE Solutions.Deleted = 0 AND Solutions.Enabled = 1 AND Solutions.Language = @language
  GROUP BY Solutions.Id, Solutions.Title
) t
WHERE NOT (t.type = '{0}' AND t.id = @id)
ORDER BY score DESC
;
";

        private ApplicationDbContext _context;

        public RelatedItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContentItem>> GetRelatedItems(int id, Enumeration.ContentItemType type, string language)
        {
            var contentItems = new List<ContentItem>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = string.Format(RelatedItemQuery, type.ToString());
                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("language", language));

                await _context.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        contentItems.Add( new ContentItem
                        {
                            Type = Enum.Parse<Enumeration.ContentItemType>(result["type"] as string),
                            Id = (int) result["Id"],
                            Title = result["Title"] as string,
                            PhotoPath = result["PhotoPath"] as string,
                            Score = decimal.ToDouble(result["Score"] as decimal? ?? 0),
                        });
                    }
                }
            }

            return contentItems;
        }
    }
}
