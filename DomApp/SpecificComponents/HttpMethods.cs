using MainComponents;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DomApp
{
    public partial class MainWindow
    {
        private async Task LoadValuesOfDom()
        {
            try
            {
                string url = ApplicationSettings.MyWebsite + "dom.json";

                // Inicjalizacja HttpClient <- zezwalanie na brak certyfikatu na stronie www
                HttpClientHandler handler = new()
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpClient client = new(handler);

                // Pobranie danych z URL
                HttpResponseMessage response = await client.GetAsync(url);

                // Sprawdzenie, czy odpowiedź jest sukcesem (status 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Odczytanie zawartości odpowiedzi jako tekst
                    string jsonData = await response.Content.ReadAsStringAsync();

                    // Deserializacja JSON do listy obiektów DomModel
                    List<DomModel>? domList = JsonSerializer.Deserialize<List<DomModel>>(jsonData);

                    if (domList != null) foreach (var item in domList)
                        {
                            foreach (var control in FindVisualChildren<Slider>(this)) // Szuka tylko przycisków; można to rozszerzyć
                            {
                                if (control.Name == item.Param)
                                {
                                    control.Value = item.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0;
                                }
                            }
                        }
                }
                else
                {
                    Logger.Write("Error: Nie można pobrać pliku JSON.");
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.Log(ex, "HttpMethods", "LoadValuesOfDom");
                debug.Content = ex.Message;
            }
        }

        // Pomocnicza funkcja do szukania elementów w drzewie wizualnym
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is not null and T)
                    {
                        yield return (T)child;
                    }

#pragma warning disable CS8604 // Możliwy argument odwołania o wartości null.
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
#pragma warning restore CS8604 // Możliwy argument odwołania o wartości null.
                }
            }
        }

        private async Task SendRequest(string param, bool value)
        {
            try
            {
                string url = ApplicationSettings.MyWebsite + "dom.php?keyID=" + ApplicationSettings.KeyID + "&param=" + param + "&value=" + value;

                HttpClientHandler handler = new()
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpClient client = new(handler);
                HttpResponseMessage response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //string responseBody =  = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                ExceptionManagement.Log(ex, "HttpMethods", "SendRequest");
                debug.Content = ex.Message;
            }
        }
    }
}