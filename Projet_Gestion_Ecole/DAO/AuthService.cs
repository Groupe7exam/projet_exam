using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace Projet_Gestion_Ecole.DAO
{
    class AuthService
    {
        private const string accountSid = "TON_ACCOUNT_SID";
        private const string authToken = "TON_AUTH_TOKEN";
        private const string twilioPhoneNumber = "TON_NUMÉRO_TWILIO";

        public static string GenererOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Génère un code à 6 chiffres
        }

        public static void EnvoyerOTP(string numero, string otp)
        {
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                to: new PhoneNumber(numero),
                from: new PhoneNumber(twilioPhoneNumber),
                body: $"Votre code de vérification est : {otp}"
            );
        }

        public static void StockerOTP(int userId, string otp)
        {
            using (var db = new DBconnect())
            {
                var otCode = new OTCode
                {
                    IdUtilisateur = userId,
                    Code = otp,
                    DateExpiration = DateTime.Now.AddMinutes(5) // Expiration après 5 minutes
                };

                db.OTCodes.Add(otCode);
                db.SaveChanges();
            }
        }
    }
}
