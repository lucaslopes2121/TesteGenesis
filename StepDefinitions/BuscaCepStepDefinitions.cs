using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace BuscaoCep.StepDefinitions
{
    [Binding]
    public class BuscaCepStepDefinitions
    {
        private IWebDriver? driver;

        [Given(@"que acesso o site dos Correios")]
        public void GivenQueAcessoOSiteDosCorreios()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/app/endereco/index.php?t");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [When(@"realizo uma busca pelo cep invalido")]
        public void WhenRealizoUmaBuscaPeloCepInvalido()
        {
            driver.FindElement(By.Id("endereco")).SendKeys("80700000");
            driver.FindElement(By.Id("btn_pesquisar")).Click();
        }

        [Then(@"o resultado deve exibir cep n�o encontrado")]
        public void ThenOResultadoDeveExibirCepNaoEncontrado()
        {
            Thread.Sleep(2000);
            Assert.AreEqual("N�o h� dados a serem exibidos", driver.FindElement(By.Id("mensagem-resultado")).Text);
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile("cepInvalido.png", ScreenshotImageFormat.Png);
            
        }

        [When(@"realizo uma busca pelo cep valido")]
        public void WhenRealizoUmaBuscaPeloCepValido()
        {
            driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/app/endereco/index.php?t");
            driver.FindElement(By.Id("endereco")).SendKeys("01013001");
            driver.FindElement(By.Id("btn_pesquisar")).Click();
        }

        [Then(@"o resultado deve exibir Rua Quinze de Novembro S�o PauloSP")]
        public void ThenOResultadoDeveExibirRuaQuinzeDeNovembroSaoPauloSP()
        {
           Thread.Sleep(2000);
            Assert.AreEqual("Rua Quinze de Novembro - lado �mpar", driver.FindElement(By.XPath("//*[@id=\"resultado-DNEC\"]/tbody/tr/td[1]")).Text);
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile("RuaNovembro.png", ScreenshotImageFormat.Png);
        }

        [When(@"realizo uma busca no rastreamento pelo c�digo")]
        public void WhenRealizoUmaBuscaNoRastreamentoPeloCodigo()
        {
            driver.Navigate().GoToUrl("https://rastreamento.correios.com.br/app/index.php");
          Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id=\"objeto\"]")).SendKeys("SS987654321BR");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("b-pesquisar")).Click();
           Thread.Sleep(2000);
        }

        [Then(@"o resultado deve exibir C�digo n�o encontrado e o browser deve ser fechado")]
        public void ThenOResultadoDeveExibirCodigoNaoEncontradoEOBrowserDeveSerFechado()
        {
            // Como tem um Captcha � um dado que � grarado dinamicamente sem um quebra Captcha n�o � poss�vel automatizar 
            // Ent�o dessa forma desidi realizar uma valida��o pelo texto Captcha inv�lido
            Thread.Sleep(2000);
            Assert.AreEqual("Preencha o campo captcha", driver.FindElement(By.XPath("/html/body/main/div[1]/form/div[2]/div[2]/div[2]/div[3]")).Text);
            driver.Quit();
        }

    }
}


