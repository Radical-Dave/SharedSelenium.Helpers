using OpenQA.Selenium;
using System;

namespace SharedSelenium
{
    public static class Helpers
    {
        public static bool PageHasContent(this IWebDriver driver, string url, bool force = false)
        {
            return driver.Search(url, "content", "*", force);
        }
        public static bool PageHasTitle(this IWebDriver driver, string url, string query, bool force = false)
        {
            return driver.Search(url, "title", query, force);
        }
        public static bool Search(this IWebDriver driver, string url, string value, string query, bool force = false)
        {
            try
            {
                if (driver.Url != url || force) driver.Navigate().GoToUrl(url);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"IWebDriverExtensions.Search({url},{value},{query},{force}) ERROR:{exception.Message}");
            }
            switch (value.ToLower())
            {
                case "title":
                    value = driver.Title.Trim();
                    break;
                default:
                    value = driver.PageSource.Trim();
                    break;
            }
            if (query == "*")
            {
                return value.Trim().Length > 0;
            }
            else if (query.StartsWith("*") && query.EndsWith("*"))
            {
                return value.Contains(query.Replace("*", ""));
            }
            else if (query.StartsWith("*"))
            {
                return value.EndsWith(query.Remove(0, 1));
            }
            else if (query.EndsWith("*"))
            {
                return value.StartsWith(query.Remove(query.Length - 1, 1));
            }
            return value == query;
        }
    }
}