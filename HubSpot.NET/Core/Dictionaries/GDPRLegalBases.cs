using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Dictionaries
{
    /// <summary>
    /// Class used for accessing the LegalBases Dictionary for subscriptions
    /// </summary>
    public class GDPRLegalBases
    {        
        private static readonly Dictionary<GDPRLegalBasis, string> LegalBases = new Dictionary<GDPRLegalBasis, string>
        {
            { GDPRLegalBasis.LEGITIMATE_INTEREST_PQL , "LEGITIMATE_INTEREST_PQL" },
            { GDPRLegalBasis.LEGITIMATE_INTEREST_CLIENT , "LEGITIMATE_INTEREST_CLIENT" },
            { GDPRLegalBasis.PERFORMANCE_OF_CONTRACT , "PERFORMANCE_OF_CONTRACT" },
            { GDPRLegalBasis.CONSENT_WITH_NOTICE , "CONSENT_WITH_NOTICE" },
            { GDPRLegalBasis.NON_GDPR , "NON_GDPR" }
        };

        public static string GetBasis(GDPRLegalBasis legalBasisKey)
            => LegalBases[legalBasisKey];
    }

    public enum GDPRLegalBasis
    {
        LEGITIMATE_INTEREST_PQL = 0,
        LEGITIMATE_INTEREST_CLIENT = 1,
        PERFORMANCE_OF_CONTRACT = 2,
        CONSENT_WITH_NOTICE = 3,
        NON_GDPR = 4
    }
}
