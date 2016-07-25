using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GzipSample
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntity
    {
        private string _entityId;

        [DataMember]
        public virtual string Identifier
        {
            get;
            set;
        }

        [DataMember]
        public virtual ExtendedEntityRow ExtendedRow
        {
            get;
            set;
        }

        [DataMember]
        public virtual string EntityIdentifier
        {
            get;
            set;
        }

        [DataMember]
        public virtual bool IsTrackingOn
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    public class ExtendedEntityColumn
    {
        [DataMember]
        public string Id
        {
            get;
            set;
        }

        [DataMember]
        public string EntityFieldIdentifier
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }

        public ExtendedEntityColumn(string entityFieldIdentifier, string value)
        {
            this.EntityFieldIdentifier = entityFieldIdentifier;
            this.Value = value;
        }

        public ExtendedEntityColumn(string entityFieldIdentifier)
        {
            this.EntityFieldIdentifier = entityFieldIdentifier;
        }

        public ExtendedEntityColumn()
        {
        }
    }

    [DataContract]
    [Serializable]
    public class ExtendedEntityRow
    {
        [DataMember]
        public string Id
        {
            get;
            set;
        }

        [DataMember]
        public string EntityIdentifier
        {
            get;
            set;
        }

        [DataMember]
        public string ReferenceId
        {
            get;
            set;
        }

        [DataMember]
        public string TenantId
        {
            get;
            set;
        }

        [DataMember]
        public Dictionary<string, ExtendedEntityColumn> ExtendedEntityColumnValues
        {
            get;
            set;
        }
    }

    [DataContract]
    public class UserDetails: BaseEntity
    {
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public MembershipDetails Membership { get; set; }
        [DataMember]
        public Address Address { get; set; }
    }

    [DataContract]
    public class User : BaseEntity
    {
        [DataMember]
        public string UserId
        {
            get;
            set;
        }

        [DataMember]
        public string MembershipId
        {
            get;
            set;
        }

        [DataMember]
        public string FirstName
        {
            get;
            set;
        }

        [DataMember]
        public string LastName
        {
            get;
            set;
        }

        [DataMember]
        public string AddressId
        {
            get;
            set;
        }

        [DataMember]
        public DateTime CreatedOn
        {
            get;
            set;
        }

        [DataMember]
        public string CreatedBy
        {
            get;
            set;
        }

        [DataMember]
        public DateTime UpdatedOn
        {
            get;
            set;
        }

        [DataMember]
        public string UpdatedBy
        {
            get;
            set;
        }

        [DataMember]
        public bool? Status
        {
            get;
            set;
        }

        [DataMember]
        public string User_Description
        {
            get;
            set;
        }

        [DataMember]
        public string TenantId
        {
            get;
            set;
        }

    }

    [Serializable]
    public class Address : BaseEntity
    {
        [DataMember]
        public string AddressId
        {
            get;
            set;
        }

        [DataMember]
        public string Address1
        {
            get;
            set;
        }

        [DataMember]
        public string Address2
        {
            get;
            set;
        }

        [DataMember]
        public string City
        {
            get;
            set;
        }

        [DataMember]
        public string State
        {
            get;
            set;
        }

        [DataMember]
        public string CountryId
        {
            get;
            set;
        }

        [DataMember]
        public string CountryName
        {
            get;
            set;
        }

        [DataMember]
        public string PostalCode
        {
            get;
            set;
        }

        [DataMember]
        public string CreatedBy
        {
            get;
            set;
        }

        [DataMember]
        public DateTime CreatedOn
        {
            get;
            set;
        }

        [DataMember]
        public string UpdatedBy
        {
            get;
            set;
        }

        [DataMember]
        public DateTime UpdatedOn
        {
            get;
            set;
        }

        [DataMember]
        public bool? Status
        {
            get;
            set;
        }

        [DataMember]
        public string UniqueId
        {
            get;
            set;
        }
    }

    [Serializable]
    public class MembershipDetails : BaseEntity
    {
        [DataMember]
        public bool IsUserPasswordChangeForced
        {
            get;
            set;
        }

        [DataMember]
        public string MembershipId
        {
            get;
            set;
        }

        [DataMember]
        public string UserName
        {
            get;
            set;
        }

        [DataMember]
        public string EmailId
        {
            get;
            set;
        }

        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        [DataMember]
        public string Comment
        {
            get;
            set;
        }

        [DataMember]
        public string Password
        {
            get;
            set;
        }

        [DataMember]
        public string PasswordQuestion
        {
            get;
            set;
        }

        [DataMember]
        public string PasswordAnswer
        {
            get;
            set;
        }

        [DataMember]
        public bool IsApproved
        {
            get;
            set;
        }

        [DataMember]
        public DateTime LastActivityDate
        {
            get;
            set;
        }

        [DataMember]
        public DateTime LastLogonDate
        {
            get;
            set;
        }

        [DataMember]
        public DateTime LastPasswordChangedDate
        {
            get;
            set;
        }

        [DataMember]
        public DateTime CreationDate
        {
            get;
            set;
        }

        [DataMember]
        public bool IsLockedOut
        {
            get;
            set;
        }

        [DataMember]
        public DateTime LastLockedOutDate
        {
            get;
            set;
        }

        [DataMember]
        public int FailedPasswordAttemptCount
        {
            get;
            set;
        }

        [DataMember]
        public DateTime FailedPasswordAttemptWindowStart
        {
            get;
            set;
        }

        [DataMember]
        public int FailedPasswordAnswerAttemptCount
        {
            get;
            set;
        }

        [DataMember]
        public DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get;
            set;
        }

        [DataMember]
        public bool? Status
        {
            get;
            set;
        }

        [DataMember]
        public bool IsFirstTimeUser
        {
            get;
            set;
        }
    }
}
