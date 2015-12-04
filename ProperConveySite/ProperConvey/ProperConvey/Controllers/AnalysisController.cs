using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;

using Google.Apis.Drive.v2.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using ProperConvey.ADO;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;

namespace ProperConvey.Controllers
{
    public class AnalysisController : Controller
    {
        
        static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Analise()
        {
            IList<string> titulos = new List<string>();

            var ClientID = "944163479234-p21idgkjpoc6ik6f1a5vv0jltnofgjgj.apps.googleusercontent.com";
            var ClientSecret = "0sxuD1VqYXS3ZgFbOU5tbv7y";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = ClientID,
                ClientSecret = ClientSecret
            },
                Scopes,
                Environment.UserName,
                CancellationToken.None,
                new FileDataStore("Daimto.GoogleDrive.Auth.Store")).Result;



            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Proper Convey Static",
            });

            FilesResource.ListRequest request = service.Files.List();
            request.Q = "trashed=false and mimeType != 'application/vnd.google-apps.folder' and title contains 'ppcn'";
            request.MaxResults = 100;
            FileList files = request.Execute();
            foreach (Google.Apis.Drive.v2.Data.File file in files.Items) {
                titulos.Add(file.Title);
            }

            return View("Analise",titulos);

        }



    }

}