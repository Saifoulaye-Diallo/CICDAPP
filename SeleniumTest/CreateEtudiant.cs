using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost:5151/Etudiants/Create");
        }

        [Test]
        public void CreationEtudiant()
        {
            // Entrer le nom
            driver.FindElement(By.Id("Nom")).SendKeys("Diallo");
            // Entrer le prenom
            driver.FindElement(By.Id("Prenom")).SendKeys("Saifoulaye");
            //Entrer l'email
            string email = "saifoulayediallo@gmail.com";
            driver.FindElement(By.Id("Email")).SendKeys(email);
            // Cliquer sur le Radio Homme
            driver.FindElement(By.XPath("//input[@type='radio' and @value='Homme']")).Click();
            // Entrer la date de naissance
            driver.FindElement(By.Name("DateNais")).SendKeys("2000-03-24");
            // Soumettre le formulaire
            driver.FindElement(By.Id("Enregistrer")).Click();

            // Verifier si l'Etudiant a bien ete ajouter
            // Attendre que l'URL change
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/"));
            
            // Recuperer le tableau d'etudiant
            var table = driver.FindElement(By.ClassName("table"));

            // Recuperer toutes les ligne du tableau
            var rows = table.FindElements(By.TagName("tr"));

            // Email à vérifier
            bool emailTrouver = rows.Any(row => row.FindElements(By.TagName("td"))
                                              .Any(cell => cell.Text == email));

            // Utiliser l'assertion pour vérifier la présence de l'email
            Assert.IsTrue(false, $"L'email {email} n'a pas été trouvé dans le tableau.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
