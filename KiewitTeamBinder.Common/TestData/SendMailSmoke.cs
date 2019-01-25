using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class SendMailSmoke
    {
        public string ProjectNumber = "AUTO2";
        public string ConnectingProduct = "KiewitApiTest";
        public string DraftBox = "Drafts";
        public string Inbox = "Inbox";
        public string Subject = "Automation send mail API";
        public string MailType = "PCO";
        public string ComposeMailAcction = "New";
        public int IntKeyForNewMail = 0;
        public int RecipientIntKey = 41;

        public string[] FilterOptions = new string[] { "{'Field': 'Subject', 'Condition': 'LIKE', 'Value': 'test'}" };

        public string MailDetailXml(int intKey, int intAddress)
        {
            return $"<DataSet xmlns='http://www.qa-software.com/'><xs:schema id='NewDataSet' xmlns='' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:msdata='urn:schemas-microsoft-com:xml-msdata'><xs:element name='NewDataSet' msdata:IsDataSet='true' msdata:UseCurrentLocale='true'><xs:complexType><xs:choice minOccurs='0' maxOccurs='unbounded'><xs:element name='fMailDetails'><xs:complexType><xs:sequence><xs:element name='Int_key' type='xs:int' minOccurs='0' /><xs:element name='Type' type='xs:string' minOccurs='0' /><xs:element name='DocumentNo' type='xs:string' minOccurs='0' /><xs:element name='Subject' type='xs:string' minOccurs='0' /><xs:element name='RespondBy' msdata:DateTimeMode='Unspecified' type='xs:dateTime' minOccurs='0' /><xs:element name='RespondedOn' msdata:DateTimeMode='Unspecified' type='xs:dateTime' minOccurs='0' /><xs:element name='Status' type='xs:string' minOccurs='0' /><xs:element name='Dated' msdata:DateTimeMode='Unspecified' type='xs:dateTime' minOccurs='0' /><xs:element name='ToCompId' type='xs:string' minOccurs='0' /><xs:element name='ToId' type='xs:string' minOccurs='0' /><xs:element name='FromCompId' type='xs:string' minOccurs='0' /><xs:element name='FromId' type='xs:string' minOccurs='0' /><xs:element name='Discipline' type='xs:string' minOccurs='0' /><xs:element name='Area' type='xs:string' minOccurs='0' /><xs:element name='Location' type='xs:string' minOccurs='0' /><xs:element name='RecptReqd' type='xs:int' minOccurs='0' /><xs:element name='Footer' type='xs:string' minOccurs='0' /><xs:element name='Priority' type='xs:int' minOccurs='0' /><xs:element name='Detail' type='xs:string' minOccurs='0' /><xs:element name='MailHtml' type='xs:string' minOccurs='0' /><xs:element name='RichText' type='xs:int' minOccurs='0' /><xs:element name='Confidential' type='xs:int' minOccurs='0' /></xs:sequence></xs:complexType></xs:element><xs:element name='fMailCustomData'><xs:complexType><xs:sequence><xs:element name='Amount' type='xs:string' minOccurs='0' /></xs:sequence></xs:complexType></xs:element><xs:element name='fMailAttachments'><xs:complexType><xs:sequence><xs:element name='Int_Key' type='xs:int' minOccurs='0' /><xs:element name='Int_Mail' type='xs:int' minOccurs='0' /><xs:element name='Int_Box' type='xs:int' minOccurs='0' /><xs:element name='FileName' type='xs:string' minOccurs='0' /><xs:element name='StoreName' type='xs:string' minOccurs='0' /><xs:element name='FileSize' type='xs:decimal' minOccurs='0' /><xs:element name='Type' type='xs:string' minOccurs='0' /><xs:element name='DocumentNo' type='xs:string' minOccurs='0' /></xs:sequence></xs:complexType></xs:element><xs:element name='fMailRecipients' msdata:CaseSensitive='False' msdata:Locale='en-US'><xs:complexType><xs:sequence><xs:element name='Int_Key' type='xs:int' minOccurs='0' /><xs:element name='Sequence' type='xs:int' minOccurs='0' /><xs:element name='ToId' type='xs:string' minOccurs='0' /><xs:element name='CompanyId' type='xs:string' minOccurs='0' /><xs:element name='Int_Cc' type='xs:int' minOccurs='0' /><xs:element name='Int_Adr' type='xs:int' minOccurs='0' /></xs:sequence></xs:complexType></xs:element></xs:choice></xs:complexType></xs:element></xs:schema><diffgr:diffgram xmlns:msdata='urn:schemas-microsoft-com:xml-msdata' xmlns:diffgr='urn:schemas-microsoft-com:xml-diffgram-v1'><NewDataSet xmlns=''><fMailDetails diffgr:id='fMailDetails1' msdata:rowOrder='0'><Int_key>{intKey}</Int_key><Type>PCO</Type><DocumentNo>PCO-SMOKE-00005.00</DocumentNo><Subject>test</Subject><RespondBy>1900-01-01T00:00:00</RespondBy><RespondedOn>1900-01-01T00:00:00</RespondedOn><Status>OUTSTANDING</Status><Dated>1900-01-01T00:00:00</Dated><ToCompId /><ToId /><FromCompId>KIEWIT</FromCompId><FromId>ADMIN1</FromId><Discipline /><Area /><Location /><RecptReqd>0</RecptReqd><Footer /><Priority>1</Priority><Detail /><MailHtml /><RichText>1</RichText><Confidential>0</Confidential></fMailDetails><fMailCustomData diffgr:id='fMailCustomData1' msdata:rowOrder='0'><Amount /></fMailCustomData><fMailRecipients diffgr:id='fMailRecipients1' msdata:rowOrder='0'><Int_Key>0</Int_Key><ToId>ADMIN1</ToId><CompanyId>Kiewit</CompanyId><Int_Cc>0</Int_Cc><Int_Adr>{intAddress}</Int_Adr></fMailRecipients></NewDataSet></diffgr:diffgram></DataSet>";
        }
    }
}
