using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzipSample
{
    public static class NumericalExtensions
    {
        public static double GetMilliSecondsFromTicks(this double ticks)
        {
            // 1 milli second = 10,000 ticks !!!
            return ticks / 10000;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //LegacyTest();

            var us = new JSONStoreBasedUserService();
            int i = 10;

            us.Add(RandomUser);
            Console.WriteLine("Object Count: " + 1 + " Time [ms]: " + PerfTimer._perfDataStore.Values.Average().GetMilliSecondsFromTicks());
            /*
            us.AddWithCompression(RandomUser);
            Console.WriteLine("Object Count: " + 1 + " Time [ms]: " + PerfTimer._perfDataStore.Values.Average().GetMilliSecondsFromTicks());
            */
            /*Console.WriteLine("Doing plain json serialization and storing");
            PlainJsonSerializationAndStore(us, i);
            PlainJsonSerializationAndStore(us, i * 100);
            PlainJsonSerializationAndStore(us, i * 1000);
            PlainJsonSerializationAndStore(us, i * 10000);
            
            Console.WriteLine("Doing compressed json serialization and storing");
            i = 10;
            CompressedJsonSerialization(us, i);
            CompressedJsonSerialization(us, i * 100);
            CompressedJsonSerialization(us, i * 1000);
            CompressedJsonSerialization(us, i * 10000);
            */

            /*
            Console.WriteLine("Deserializing plain Json back to object");
            i = 10;
            DeserializePlainJson(us, i);
            DeserializePlainJson(us, i * 100);
            DeserializePlainJson(us, i * 1000);
            DeserializePlainJson(us, i * 10000);
            
          
            Console.WriteLine("deserializing the Decompressed stored compressed json and back to object");
            i = 10;
            DeserializeCompressedJson(us, i);
            DeserializeCompressedJson(us, i * 100);
            DeserializeCompressedJson(us, i * 1000);
            DeserializeCompressedJson(us, i * 10000);
            */
            Console.ReadLine();
        }

        private static void CompressedJsonSerialization(JSONStoreBasedUserService us, int ct)
        {
            for (int i = 0; i < ct; i++) us.AddWithCompression(RandomUser);
            Console.WriteLine("Object Count: " + ct + " Time [ms]: " + PerfTimer._perfDataStore.Values.Average().GetMilliSecondsFromTicks());
            PerfTimer._perfDataStore.Clear();
            UserStore._userStore.Clear();
        }

        private static void PlainJsonSerializationAndStore(JSONStoreBasedUserService us, int ct)
        {
            for (int i = 0; i < ct; i++) us.Add(RandomUser);
            Console.WriteLine("Object Count: " + ct + " Time [ms]: " + PerfTimer._perfDataStore.Values.Average().GetMilliSecondsFromTicks());
            PerfTimer._perfDataStore.Clear();
            UserStore._userStore.Clear();
        }

        private static void DeserializePlainJson(JSONStoreBasedUserService us, int ct)
        {
            for (int i = 0; i < ct; i++) us.Add(RandomUser);
            PerfTimer._perfDataStore.Clear();
            foreach (var item in us.GetAll()) { }
            Console.WriteLine("Object Count : " + ct + " Time [ms]: " + PerfTimer._perfDataStore.Values.Average().GetMilliSecondsFromTicks());
            PerfTimer._perfDataStore.Clear();
            UserStore._userStore.Clear();
        }

        private static void DeserializeCompressedJson(JSONStoreBasedUserService us, int ct)
        {
            for (int i = 0; i < ct; i++) us.AddWithCompression(RandomUser);
            PerfTimer._perfDataStore.Clear();
            foreach (var item in us.GetAllUnCompressed()) { }
            Console.WriteLine("Object Count : " + ct + " Time [ms]: " + PerfTimer._perfDataStore.Values.Average().GetMilliSecondsFromTicks());
            PerfTimer._perfDataStore.Clear();
            UserStore._userStore.Clear();
        }

        public static UserDetails RandomUser
        {
            get
            {
                return new UserDetails
                {
                    Address = new Address
                    {
                        Address1 = Path.GetRandomFileName(),
                        Address2 = Path.GetRandomFileName(),
                        AddressId = Path.GetRandomFileName(),
                        City = Path.GetRandomFileName(),
                        CountryId = Path.GetRandomFileName(),
                        CountryName = Path.GetRandomFileName(),
                        CreatedBy = Path.GetRandomFileName(),
                        CreatedOn = DateTime.Now,
                        EntityIdentifier = Path.GetRandomFileName(),
                        Identifier = Guid.NewGuid().ToString(),
                        IsTrackingOn = false,
                        PostalCode = Path.GetRandomFileName(),
                        State = Path.GetRandomFileName(),
                        Status = true,
                        UniqueId = Guid.NewGuid().ToString(),
                        UpdatedOn = DateTime.MinValue
                    },
                    EntityIdentifier = Path.GetRandomFileName(),
                    Identifier = Guid.NewGuid().ToString(),
                    IsTrackingOn = true,
                    Membership = new MembershipDetails
                    {
                        Comment = Path.GetRandomFileName(),
                        CreationDate = DateTime.Now,
                        EmailId = Path.GetRandomFileName() + "@company.com",
                        EntityIdentifier = Path.GetRandomFileName(),
                        FailedPasswordAnswerAttemptCount = int.MaxValue,
                        FailedPasswordAnswerAttemptWindowStart = DateTime.Now,
                        FailedPasswordAttemptCount = int.MaxValue,
                        FailedPasswordAttemptWindowStart = DateTime.Now,
                        Identifier = Guid.NewGuid().ToString(),
                        IsApproved = false,
                        IsFirstTimeUser = true,
                        IsLockedOut = false,
                        IsTrackingOn = false,
                        IsUserPasswordChangeForced = false,
                        LastActivityDate = DateTime.MinValue,
                        LastLockedOutDate = DateTime.MaxValue,
                        LastLogonDate = DateTime.Now,
                        LastPasswordChangedDate = DateTime.UtcNow,
                        MembershipId = Guid.NewGuid().ToString(),
                        Password = Path.GetRandomFileName(),
                        PasswordAnswer = Path.GetRandomFileName(),
                        PasswordQuestion = Path.GetRandomFileName(),
                        Status = true,
                        TenantCode = Guid.NewGuid().ToString(),
                        UserName = Path.GetRandomFileName(),
                    },
                    User = new User
                    {
                        AddressId = NewId,
                        CreatedBy = NewId,
                        CreatedOn = DateTime.UtcNow,
                        EntityIdentifier = Path.GetRandomFileName(),
                        FirstName = Path.GetRandomFileName(),
                        Identifier = NewId,
                        IsTrackingOn = true,
                        LastName = Path.GetRandomFileName(),
                        MembershipId = NewId,
                        Status = true,
                        TenantId = NewId,
                        UpdatedBy = NewId,
                        UpdatedOn = DateTime.UtcNow,
                        User_Description = Path.GetRandomFileName(),
                        UserId = NewId
                    }
                };
            }

        }

        private static string NewId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        private static void LegacyTest()
        {
            string input = "<UserDetails xmlns:i='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.UserManagement'><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>User</EntityIdentifier><ExtendedRow xmlns:d2p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>3398f837-b988-4708-999d-d3dfe11875b3</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><Address xmlns:d2p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.TenantManagement'><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>Address</EntityIdentifier><ExtendedRow xmlns:d3p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><d2p1:Address1 i:nil='true'/><d2p1:Address2 i:nil='true'/><d2p1:AddressId i:nil='true'/><d2p1:City i:nil='true'/><d2p1:CountryId i:nil='true'/><d2p1:CountryName i:nil='true'/><d2p1:CreatedBy i:nil='true'/><d2p1:CreatedOn>0001-01-01T00:00:00</d2p1:CreatedOn><d2p1:PostalCode i:nil='true'/><d2p1:State i:nil='true'/><d2p1:Status i:nil='true'/><d2p1:UniqueId i:nil='true'/><d2p1:UpdatedBy i:nil='true'/><d2p1:UpdatedOn>0001-01-01T00:00:00</d2p1:UpdatedOn></Address><MembershipDetails><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>MembershipDetails</EntityIdentifier><ExtendedRow xmlns:d3p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>4ad211a6-e845-43e9-a668-6f5bdbfb7f3c</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><Comment>Product admin user</Comment><CreationDate>2015-10-06T12:03:27.53</CreationDate><EmailId>admin@p3datasys.com</EmailId><FailedPasswordAnswerAttemptCount>0</FailedPasswordAnswerAttemptCount><FailedPasswordAnswerAttemptWindowStart>0001-01-01T00:00:00</FailedPasswordAnswerAttemptWindowStart><FailedPasswordAttemptCount>0</FailedPasswordAttemptCount><FailedPasswordAttemptWindowStart>0001-01-01T00:00:00</FailedPasswordAttemptWindowStart><IsApproved>true</IsApproved><IsFirstTimeUser>false</IsFirstTimeUser><IsLockedOut>false</IsLockedOut><IsUserPasswordChangeForced>false</IsUserPasswordChangeForced><LastActivityDate>2015-11-12T18:42:08.48</LastActivityDate><LastLockedOutDate>0001-01-01T00:00:00</LastLockedOutDate><LastLogonDate>2015-11-12T18:42:08.48</LastLogonDate><LastPasswordChangedDate>0001-01-01T00:00:00</LastPasswordChangedDate><MembershipId>4ad211a6-e845-43e9-a668-6f5bdbfb7f3c</MembershipId><Password i:nil='true'/><PasswordAnswer/><PasswordQuestion/><Status>true</Status><TenantCode>b590cd25-3093-df11-8deb-001ec9dab123</TenantCode><UserName>admin@p3datasys.com</UserName></MembershipDetails><User><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>User</EntityIdentifier><ExtendedRow xmlns:d3p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>3398f837-b988-4708-999d-d3dfe11875b3</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><AddressId i:nil='true'/><CreatedBy>3398f837-b988-4708-999d-d3dfe11875b3</CreatedBy><CreatedOn>2015-10-06T12:03:27.54</CreatedOn><FirstName>Administrator</FirstName><LastName>P3 Data Systems</LastName><MembershipId>4ad211a6-e845-43e9-a668-6f5bdbfb7f3c</MembershipId><Status>true</Status><TenantId>b590cd25-3093-df11-8deb-001ec9dab123</TenantId><UpdatedBy i:nil='true'/><UpdatedOn>0001-01-01T00:00:00</UpdatedOn><UserId>3398f837-b988-4708-999d-d3dfe11875b3</UserId><User_Description>Product admin user</User_Description></User></UserDetails>";


            input = "<Tenant xmlns:i='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.TenantManagement'><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>Tenant</EntityIdentifier><ExtendedRow xmlns:d2p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>2acb26b9-4f77-e511-8b08-d4bed9432042</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><Address i:nil='true'/><ContactDetail xmlns:d2p1='CelloSaaS.Model.TenantManagement' i:nil='true'/><TenantAdminMembershipdetail xmlns:d2p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.UserManagement'><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>MembershipDetails</EntityIdentifier><ExtendedRow xmlns:d3p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>30cb26b9-4f77-e511-8b08-d4bed9432042</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><d2p1:Comment i:nil='true'/><d2p1:CreationDate>2015-10-20T22:56:42.4151404+05:30</d2p1:CreationDate><d2p1:EmailId>admin@srpl.xom</d2p1:EmailId><d2p1:FailedPasswordAnswerAttemptCount>0</d2p1:FailedPasswordAnswerAttemptCount><d2p1:FailedPasswordAnswerAttemptWindowStart>0001-01-01T00:00:00</d2p1:FailedPasswordAnswerAttemptWindowStart><d2p1:FailedPasswordAttemptCount>0</d2p1:FailedPasswordAttemptCount><d2p1:FailedPasswordAttemptWindowStart>0001-01-01T00:00:00</d2p1:FailedPasswordAttemptWindowStart><d2p1:IsApproved>true</d2p1:IsApproved><d2p1:IsFirstTimeUser>true</d2p1:IsFirstTimeUser><d2p1:IsLockedOut>false</d2p1:IsLockedOut><d2p1:IsUserPasswordChangeForced>false</d2p1:IsUserPasswordChangeForced><d2p1:LastActivityDate>0001-01-01T00:00:00</d2p1:LastActivityDate><d2p1:LastLockedOutDate>0001-01-01T00:00:00</d2p1:LastLockedOutDate><d2p1:LastLogonDate>0001-01-01T00:00:00</d2p1:LastLogonDate><d2p1:LastPasswordChangedDate>0001-01-01T00:00:00</d2p1:LastPasswordChangedDate><d2p1:MembershipId i:nil='true'/><d2p1:Password>aDBNcOuxPxZn5YdeCU6E4Q==</d2p1:Password><d2p1:PasswordAnswer i:nil='true'/><d2p1:PasswordQuestion i:nil='true'/><d2p1:Status i:nil='true'/><d2p1:TenantCode>2acb26b9-4f77-e511-8b08-d4bed9432042</d2p1:TenantCode><d2p1:UserName>admin@srpl.xom</d2p1:UserName></TenantAdminMembershipdetail><TenantAdminUserdetail xmlns:d2p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.UserManagement'><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>User</EntityIdentifier><ExtendedRow xmlns:d3p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>31cb26b9-4f77-e511-8b08-d4bed9432042</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><d2p1:AddressId i:nil='true'/><d2p1:CreatedBy>2cfd0666-a16e-e511-86d3-d4bed9432042</d2p1:CreatedBy><d2p1:CreatedOn>2015-10-20T22:56:42.4151404+05:30</d2p1:CreatedOn><d2p1:FirstName>admin</d2p1:FirstName><d2p1:LastName>srpl</d2p1:LastName><d2p1:MembershipId>30cb26b9-4f77-e511-8b08-d4bed9432042</d2p1:MembershipId><d2p1:Status i:nil='true'/><d2p1:TenantId>2acb26b9-4f77-e511-8b08-d4bed9432042</d2p1:TenantId><d2p1:UpdatedBy i:nil='true'/><d2p1:UpdatedOn>0001-01-01T00:00:00</d2p1:UpdatedOn><d2p1:UserId>31cb26b9-4f77-e511-8b08-d4bed9432042</d2p1:UserId><d2p1:User_Description i:nil='true'/></TenantAdminUserdetail><TenantDetails><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>Tenant</EntityIdentifier><ExtendedRow xmlns:d3p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>2acb26b9-4f77-e511-8b08-d4bed9432042</Identifier><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><AddressID i:nil='true'/><ApprovalStatus>WaitingForApproval</ApprovalStatus><BillOther>false</BillOther><BillingAddressId i:nil='true'/><BillingContactId i:nil='true'/><BillingType i:nil='true'/><CompanySize i:nil='true'/><ContactId i:nil='true'/><CreatedBy i:nil='true'/><CreatedOn>0001-01-01T00:00:00</CreatedOn><DataPartitionId i:nil='true'/><Description i:nil='true'/><EnableAutoDebit>false</EnableAutoDebit><IsProductAdmin>false</IsProductAdmin><IsSelfRegistered>false</IsSelfRegistered><ParentTenant><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>Tenant</EntityIdentifier><ExtendedRow xmlns:d4p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><AddressID i:nil='true'/><ApprovalStatus i:nil='true'/><BillOther>false</BillOther><BillingAddressId i:nil='true'/><BillingContactId i:nil='true'/><BillingType i:nil='true'/><CompanySize i:nil='true'/><ContactId i:nil='true'/><CreatedBy i:nil='true'/><CreatedOn>0001-01-01T00:00:00</CreatedOn><DataPartitionId i:nil='true'/><Description i:nil='true'/><EnableAutoDebit>false</EnableAutoDebit><IsProductAdmin>false</IsProductAdmin><IsSelfRegistered>false</IsSelfRegistered><ParentTenant i:nil='true'/><ParentTenantId i:nil='true'/><PaymentType i:nil='true'/><Status i:nil='true'/><TenantCode>26fd0666-a16e-e511-86d3-d4bed9432042</TenantCode><TenantCodeString i:nil='true'/><TenantName i:nil='true'/><URL i:nil='true'/><UpdatedBy i:nil='true'/><UpdatedOn>0001-01-01T00:00:00</UpdatedOn><Website i:nil='true'/><childTenant xmlns:d4p1='http://schemas.microsoft.com/2003/10/Serialization/Arrays' i:nil='true'/></ParentTenant><ParentTenantId>26fd0666-a16e-e511-86d3-d4bed9432042</ParentTenantId><PaymentType i:nil='true'/><Status>false</Status><TenantCode>2acb26b9-4f77-e511-8b08-d4bed9432042</TenantCode><TenantCodeString>srpl</TenantCodeString><TenantName>southern regional premier league</TenantName><URL i:nil='true'/><UpdatedBy i:nil='true'/><UpdatedOn>0001-01-01T00:00:00</UpdatedOn><Website i:nil='true'/><childTenant xmlns:d3p1='http://schemas.microsoft.com/2003/10/Serialization/Arrays' i:nil='true'/></TenantDetails><Types><TenantType><EntityIdentifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>TenantType</EntityIdentifier><ExtendedRow xmlns:d4p1='http://schemas.datacontract.org/2004/07/CelloSaaS.Model.DataManagement' xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><Identifier xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model' i:nil='true'/><IsTrackingOn xmlns='http://schemas.datacontract.org/2004/07/CelloSaaS.Model'>false</IsTrackingOn><AccessLevel>0</AccessLevel><CreatedBy i:nil='true'/><CreatedOn>0001-01-01T00:00:00</CreatedOn><ID>88144449-0a44-4da7-950c-d18c937dcf44</ID><Name i:nil='true'/><Status>false</Status><UpdatedBy i:nil='true'/><UpdatedOn>0001-01-01T00:00:00</UpdatedOn></TenantType></Types></Tenant>";
            string res = CompressionWrapper.Compress(input);
            //Console.WriteLine(res);
            Console.WriteLine("uncompress length : " + input.Length + " , compressed length: " + res.Length);


            string unCompressed = CompressionWrapper.DeCompress(res);

            if (input == unCompressed)
                Console.WriteLine("before and after compression, the content still remains the same ");
        }
    }


}
