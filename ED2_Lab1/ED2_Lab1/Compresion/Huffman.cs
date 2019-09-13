using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ED2_Lab1.Models;

namespace ED2_Lab1.Compresion
{
    public class Huffman
    {
        List<HuffmanNode> ListaHojas = new List<HuffmanNode>();

        public void GenerarArbol(List<HuffmanNode> ListaCaracteres)
        {

            while (ListaCaracteres.Count > 1)
            {
                ListaCaracteres.Sort();
                HuffmanNode hnDerecho = ListaCaracteres[0];
                ListaCaracteres.RemoveAt(0);
                HuffmanNode hnIzquierdo = ListaCaracteres[0];
                ListaCaracteres.RemoveAt(0);

                HuffmanNode nAux = new HuffmanNode();
                nAux = nAux.CrearNodoPadre(hnDerecho, hnIzquierdo);
                nAux.rightTree.parentNode = nAux;
                nAux.leftTree.parentNode = nAux;

                ListaCaracteres.Add(nAux);
            }
        }

    }
}