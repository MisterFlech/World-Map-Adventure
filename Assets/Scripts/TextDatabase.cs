using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDatabase : MonoBehaviour
{
    public static string[] frenchText =
    {
        //0
        "Je m'appelle Darturo Yakak,\n" +
            "et mon rêve, c'est de voyager un\n" +
            "jour au-delà de ces montagnes.",
        //1
        "En vrai, pourquoi attendre ? \n" +
            "Il faudrait que je fouille\n" +
            "dans le grenier de grand-père.\n",
        //2
        "Je suis sûr que je peux y trouver\n" +
            "des équipements utiles pour\n" +
            "escalader la montagne.",
        //3
        "Voyons voir ce qu'il y a dans ce coffre.",
        //4
        "A vrai dire, je n'ai jamais retouché\n" +
            "à ses affaires. Ca fait maintenant\n" +
            "un an qu'il est parti.",
        //5
        "Tiens, c'est quoi cette lampe ?",
        //6
        "Kof, kof...",

        //7
        "Que... Qui es-tu ?",
        //8
        "Mon nom est Zarwid, et j'étais\n" +
            "coincé dans cette lampe.\n" +
            "Merci de m'avoir libéré.",
        //9
        "Serais-tu... un génie ?",
        //10
        "Non, non, je suis magicien.\n",
        //11
        "Roh dommage, j'aurais bien aimé\n" +
            "avoir un voeu.",
        //12
        "Ah mais tu sais, on peut toujours\n" +
            "s'arranger ! Quel est ton voeu\n" +
            "le plus cher ?",
        //13
        "J'aimerais bien traverser les\n" +
            "montagnes et voyager dans\n" +
            "le monde entier !",
        //14
        "Eh bien, que ton voeu soit exaucé !",
        //15
        "ABRACADABRA !",
        //16
        "Euh...",
        //17
        "Il a... disparu ?",
        //18
        "OK... Mais mon voeu ?",
        //19
        "Bon, je vais voir dehors si quelque\n" +
            "chose a changé dehors.",

        //20
        "Voyons voir s'il y a quelque\n" +
            "chose de différent.",
    };

    public static string[] englishText =
    {
        //0
        "My name is Darturo Yakak, \n" +
            "and my dream is to travel\n" +
            "some day behond those mountains.",
        //1
    };

    public static string getText(int i)
    {
        if(GlobalVariables.language == (int)Language.French)
        {
            return frenchText[i];
        }
        else if (GlobalVariables.language == (int)Language.English)
        {
            return englishText[i];
        }
        return "";
    }

    public static string actualName = "";

    public static string[] names =
    {
        //0
        "",
        //1
        "Darturo",
        //2
        "Zarwid",
    };

    public static int[] nameText =
    {
        0,
        0,
        0,
        0,
        0,
        //5
        0,
        0,
        //7
        1,
        2,
        1,
        //10
        2,
        1,
        2,
        1,
        2,
        //15
        0,
        1,
        0,
        0,
        0,
        //20
        1
    };

    public static string getName(int i)
    {
        Debug.Log(i + " : " + nameText[i]);
        if (nameText[i] != 0)
        {
            Debug.Log("name : "+ actualName);
            actualName = names[nameText[i]];
            Debug.Log("new name : " + actualName);
        }

        return actualName;
    }
}
