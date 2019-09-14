using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED2_Lab1.Models
{
    public class HuffmanNode:IComparable<HuffmanNode>
    {
        public string caracter;   
        public int frecuencia;
        public decimal probability;
        public string code;           
        public HuffmanNode parentNode; 
        public HuffmanNode leftTree;   
        public HuffmanNode rightTree; 
        public bool isLeaf;           

        public HuffmanNode()
        {
            caracter = default(string);
            frecuencia = default(int);
            probability = default(decimal);
            code = default(string);
            parentNode = null;
            leftTree = null;
            rightTree = null;
            isLeaf = default(bool);
        }

        public HuffmanNode(string value)   
        {
            caracter = value;     
            frecuencia = 1;
            probability = default(decimal);
            rightTree = leftTree = parentNode = null;      

            code = "";         
            isLeaf = true;     
        }

        public void Frecuencia()             
        {
            frecuencia++;
        }

        public HuffmanNode CrearNodoPadre(HuffmanNode hnDerecho,HuffmanNode hnIzquierdo)
        {
            code = "";
            frecuencia = hnDerecho.frecuencia + hnIzquierdo.frecuencia;
            probability = hnDerecho.probability + hnIzquierdo.probability;
            isLeaf = false;

            if (hnDerecho.frecuencia >= hnIzquierdo.frecuencia)
            {
                rightTree = hnDerecho;
                leftTree = hnIzquierdo;
                caracter = hnDerecho.caracter + hnIzquierdo.caracter;
            }
            else if (hnDerecho.frecuencia < hnIzquierdo.frecuencia)
            {
                rightTree = hnIzquierdo;
                leftTree = hnDerecho;
                caracter = hnIzquierdo.caracter + hnDerecho.caracter;
            }

            return this;
        }

        public int CompareTo(HuffmanNode other)
        {
            return this.probability.CompareTo(other.probability);
        }
    }
}