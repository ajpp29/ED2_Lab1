using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED2_Lab1.Models
{
    public class HuffmanNode
    {
        public string caracter;   
        public int frecuencia;         
        public string code;           
        public HuffmanNode parentNode; 
        public HuffmanNode leftTree;   
        public HuffmanNode rightTree; 
        public bool isLeaf;           


        public HuffmanNode(string value)   
        {
            caracter = value;     
            frecuencia = 1;      

            rightTree = leftTree = parentNode = null;      

            code = "";         
            isLeaf = true;     
        }

        public void Frecuencia()             
        {
            frecuencia++;
        }
    }
}