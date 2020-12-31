using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GEC_DataLayer.Models.Enumerations.Mail;

namespace GEC_DataLayer.Models.Entities
{
    public class Mail
    {
        public long? Id { get; set; }
        public MailType MailType { get; set; }
        public Channel Channel { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public long IdSender { get; set; }
        public string SenderRegistrationNumber { get; set; }
        public DateTime? SendingDate { get; set; }
        public long IdRecipient { get; set; }
        public ProcessingTimeFrame? ProcessingTimeFrame { get; set; }
        public Confidentiality? Confidentiality { get; set; }
        public string Object { get; set; }
        public string Content { get; set; }
        public string DigitizedFile { get; set; }
        public Language? Language { get; set; }
        public string KeyWords { get; set; }
        public string Observations { get; set; }
        public long? IdFolder { get; set; }
        public bool HasHardCopy { get; set; }

        public Mail()
        {

        }

        public Mail(long? pId, MailType pMailType, Channel pChannel, DateTime pRegistrationDate, 
            string pRegistrationNumber, long pIdSender, string pSenderRegistrationNumber, DateTime? pSendingDate,
            long pIdRecipient, ProcessingTimeFrame? pProcessingTimeFrame, Confidentiality? pConfidentiality,
            string pObject, string pContent, string pDigitizedFile, Language? pLanguage, string pKeyWords, 
            string pObservations, long?pIdFolder, bool pHasHardCopy)
        {
            if(pSendingDate != null)
            {
                if(pSendingDate.Value > pRegistrationDate)
                {
                    throw new ArgumentException("Sending date must precede registration date.");
                }
            }

            if(string.IsNullOrWhiteSpace(pRegistrationNumber))
            {
                throw new ArgumentException("Registration number is required.");
            }

            if (string.IsNullOrWhiteSpace(pObject))
            {
                throw new ArgumentException("Object is required.");
            }

            Id = pId;
            MailType = pMailType;
            Channel = pChannel;
            RegistrationDate = pRegistrationDate;
            RegistrationNumber = pRegistrationNumber;
            IdSender = pIdSender;
            SenderRegistrationNumber = pSenderRegistrationNumber;
            SendingDate = pSendingDate;
            IdRecipient = pIdRecipient;
            ProcessingTimeFrame = pProcessingTimeFrame;
            Confidentiality = pConfidentiality;
            Object = pObject;
            Content = pContent;
            DigitizedFile = pDigitizedFile;
            Language = pLanguage;
            KeyWords = pKeyWords;
            Observations = pObservations;
            IdFolder = pIdFolder;
            HasHardCopy = pHasHardCopy;
        }
    }
}
