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
        HuffmanNode hnRaiz = new HuffmanNode();

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

            hnRaiz = AsignarCodigosPrefijo("", ListaCaracteres[0]);
        }

        private HuffmanNode AsignarCodigosPrefijo(string codigoprefijo, HuffmanNode hnNodoActual)
        {
            if (hnNodoActual == null)
            {
                return null;
            }
            else if (hnNodoActual.leftTree==null && hnNodoActual.rightTree == null)
            {
                hnNodoActual.code = codigoprefijo;
                return hnNodoActual;
            }

            hnNodoActual.leftTree = AsignarCodigosPrefijo(codigoprefijo + "0", hnNodoActual.leftTree);
            hnNodoActual.rightTree = AsignarCodigosPrefijo(codigoprefijo + "1", hnNodoActual.rightTree);

            return hnNodoActual;
        }

    }
}