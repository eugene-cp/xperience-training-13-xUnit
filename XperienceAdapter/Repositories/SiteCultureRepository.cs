﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CMS.Base;
using CMS.Helpers;
using CMS.Localization;
using CMS.SiteProvider;

using XperienceAdapter.Models;

namespace XperienceAdapter.Repositories
{
    public class SiteCultureRepository : ISiteCultureRepository
    {
        protected readonly ISiteService _siteService;

        public SiteCulture DefaultSiteCulture =>
            GetAll().FirstOrDefault(culture => culture.IsDefault);

        public SiteCultureRepository(ISiteService siteService, ICultureSiteInfoProvider cultureSiteInfoProvider)
        {
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
        }

        /// <summary>
        /// Maps DTO properties.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static SiteCulture MapDtoProperties(CultureInfo culture, string siteName) => new SiteCulture
        {
            FriendlyName = culture.CultureName,
            IsDefault = CultureHelper.GetDefaultCultureCode(siteName) == culture.CultureCode,
            IsoCode = culture.CultureCode,
            ShortName = culture.CultureShortName
        };

        public IEnumerable<SiteCulture> GetAll()
        {
            var siteName = _siteService.CurrentSite.SiteName;

            return CultureSiteInfoProvider.GetSiteCultures(siteName)
                .Select(culture => MapDtoProperties(culture, siteName));
        }

        public Task<IEnumerable<SiteCulture>> GetAllAsync() => Task.FromResult(GetAll());

        public SiteCulture GetByExactIsoCode(string isoCode) =>
            GetAll().FirstOrDefault(culture => culture.IsoCode?.Equals(isoCode, StringComparison.OrdinalIgnoreCase) == true);

    }
}
