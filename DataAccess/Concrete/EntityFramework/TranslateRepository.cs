using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccess.Concrete.EntityFramework
{
    public class TranslateRepository : EfEntityRepositoryBase<Translate, ProjectDbContext>, ITranslateRepository
    {
        public TranslateRepository(ProjectDbContext context) : base(context)
        {

        }
        public async Task<List<TranslateDto>> GetTranslateDtos()
        {
            var list = await (from language in context.Languages
                            join translate in context.Translates on language.Id equals translate.LangId
                            select new TranslateDto()
                            {
                                Id = translate.Id,
                                Code = translate.Code,
                                Language = language.Code,
                                Value = translate.Value
                            }).ToListAsync();
            return list;
        }

        public async Task<string> GetTranslatesByLang(string langCode)
        {
            var data = await (from translate in context.Translates
                              join language in context.Languages on translate.LangId equals language.Id
                              where language.Code == langCode
                              select translate).ToDictionaryAsync(x => (string)x.Code, x => (string)x.Value);

            var str = JsonConvert.SerializeObject(data);
            return str;
        }

        public async Task<Dictionary<string, string>> GetTranslateWordList(string lang)
        {
            var list = await context.Translates.Where(x => x.Code == lang).ToListAsync();

            return list.ToDictionary(x => x.Code, x => x.Value);
        }
    }
}
