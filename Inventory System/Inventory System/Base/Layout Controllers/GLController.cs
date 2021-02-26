using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.GUI
{
    public class GLController : MonoBehaviour
    {
        private List<Selectable> items = new List<Selectable>();
        private bool coroutineRunning = false;

        private int rows = 0, columns = 0;
        private int xMin = 0, xMax = 0;
        public void AddNavigableItem(Selectable item) => items.Add(item);
        public bool RemoveNavigableItem(Selectable item) => items.Remove(item);
        public void RecalculateNavigation()
        {
            lock (items)
            {
                if (!coroutineRunning)
                {
                    coroutineRunning = true;
                    StartCoroutine(CalculateNavigation());
                }
            }
        }
        private IEnumerator CalculateNavigation()
        {
            //ATTENDO LA FINE DEL FRAME COSì DA AVER TUTTE LE MODIFICHE AI TRANSFORM
            yield return new WaitForEndOfFrame();
            //MI RICALCOLO IL NAVIGATION X OGNI ELEMENTO
            rows = 0;
            columns = 0;
            xMin = 0;
            xMax = 0;
            for (int index = 0; index < items.Count; index++)
            {
                Selectable item = items[index];
                int xPosition = (int)item.GetComponent<Transform>().position.x;
                Navigation currentNav = new Navigation
                {
                    mode = Navigation.Mode.Explicit,
                };

                //PRIMO ELEMENTO IN ALTO A SX
                if (rows == 0)
                {
                    xMax = xMin = xPosition;
                    rows++;
                    columns++;
                    item.navigation = currentNav;
                }
                //TUTTI GLI ALTRI ELEMENTI
                else
                {
                    //SE SONO IL PRIMO ELEMENTO DELLA NUOVA RIGA
                    if (xPosition == xMin)
                    {
                        rows++;
                        //MI COLLEGO AL NAVIGATOR CHE HO DI SOPRA
                        currentNav.selectOnUp = items[index - columns];
                        item.navigation = currentNav;

                        //AGGIORNO IL NAV DELL'ELEMENTO SOPRA
                        Navigation upNav = items[index - columns].navigation;
                        upNav.selectOnDown = item;
                        items[index - columns].navigation = upNav;
                    }
                    else
                    {
                        //ELEMENTI PRIMA RIGA
                        if (rows == 1)
                        {
                            xMax = xPosition;
                            columns++;
                        }
                        //ELEMENTI SECONDA ED OLTRE RIGHE
                        if (rows > 1)
                        {
                            currentNav.selectOnUp = items[index - columns];

                            //AGGIORNO IL NAV DELL'ELEMENTO SOPRA
                            Navigation upNav = items[index - columns].navigation;
                            upNav.selectOnDown = item;
                            items[index - columns].navigation = upNav;

                        }
                        //MIO NAVIGATOR, MI COLLEGO CON QUELLO A SX
                        currentNav.selectOnLeft = items[index - 1];
                        item.navigation = currentNav;
                        //AGGIORNO IL NAV DELL'ELEMENTO DI SINISTRA
                        Navigation navSx = items[index - 1].navigation;
                        navSx.selectOnRight = item;
                        items[index - 1].navigation = navSx;
                    }
                }
            }
            coroutineRunning = false;
        }
    }
}