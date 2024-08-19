using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDatabase : MonoBehaviour
{
    public static string[] frenchText =
    {
        //0
        "Je m'appelle Darturo Yakak, et mon rêve, c'est de voyager un jour au-delà de ces montagnes.",
        //1
        "En vrai, pourquoi attendre ? Je pourrais certainement trouver des équipements utiles en fouillant dans",
        //2
        "les affaires de mon grand-père.\nJe crois qu'elles sont au grenier.\nIl a toujours aimé l'escalade !",
        //3
        "Voyons voir ce qu'il y a dans ce coffre.",
        //4
        "A vrai dire, je n'ai jamais retouché à ses affaires. Ca fait maintenant un an qu'il est parti.",
        //5
        "Tiens, c'est quoi cette lampe ?",
        //6
        "Kof, kof...",

        //7
        "Que... Qui es-tu ?",
        //8
        "Salut ! Mon nom est Zarwid ! J'étais coincé dans cette lampe. Merci de m'avoir libéré !",
        //9
        "Serais-tu... un génie ?",
        //10
        "Non, non, je suis magicien.\n",
        //11
        "Roh dommage, j'aurais bien aimé\n" +
            "avoir un voeu.",
        //12
        "Ah mais tu sais, on peut toujours s'arranger ! Quel est ton voeu le plus cher ?",
        //13
        "J'aimerais bien traverser les montagnes et voyager dans le monde entier !",
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
        "Bon, je vais voir dehors si quelque chose a changé dehors.",

        //20
        "Les montagnes sont à droite.\n" +
            "Allons-y !",

        //21
        "Tiens, c'est bizarre... ",

        //22
        "J'ai l'impression que les montagnes sont beaucoup plus petites que d'habitude.",
        //23
        "Hé mais... c'est moi qui suis géant !\n" +
            "Qu'est-ce que c'est que ce bazar ?",
        //24
        "Ben alors, c'est toi qui voulais traverser les montagnes, non ?",

        //25
        "Mais, n'est-ce pas un peu démesuré comme méthode ?!",

        //26
        "Je n'ai fait que concrétiser ton voeu.\n" +
            "Allez vas-y, essaye de pousser une montagne !",

        //27
        "Quoi ? Je peux faire ça ? Mais c'est terrible ! Je vais totalement ravager le paysage !",

        //28
        "Mais non, ne t'inquiète pas ! " +
            "La nature est plus forte que tu ne le crois !",

        //29
        "Mais... Comment je fais si je veux revenir chez moi ? " +
            "Je risque d'écraser ma maison si je retourne en arrière, non ?",

        //30
        "Eh bien non, justement. Car le sort que je t'ai jeté te permet de retrouver " +
            "ta taille normale à chaque fois que tu rentres par une ouverture humaine. " +
            "En l'occurence, ta clôture.",

        //31
        "C'est vraiment chelou comme sort...",

        //32
        "Allez, vas-y, essaye ! Tu vas voir, c'est plutôt rigolo en vrai !",

        //33
        "Tiens, c'est bizarre, il y a une rivière à côté de ma maison maintenant !",
    
        //34
        "Tiens, je n'avais jamais remarqué qu'il y avait une habitation de l'autre côté du pont !",
        //35
        "Il faudrait que j'aille lui faire coucou un de ces jours !",
    };

    public static string[] englishText =
    {
        //0
        "My name is Darturo Yakak,\n" +
            "and my dream is to one day\n" +
            "travel beyond those mountains.",
        //1
        "But really, why wait?\n" +
            "I could probably find some\n" +
            "useful equipment by rummaging\n",
        //2
        "through my grandfather's belongings.\n" +
            "I believe they’re in the attic.\n" +
            "He always loved climbing!",
        //3
        "Let’s see what’s in this chest.",
        //4
        "To be honest, I’ve never touched\n" +
            "his stuff. It’s been a year\n" +
            "since he passed.",
        //5
        "Hmm, what’s this lamp?",
        //6
        "Cough, cough...",

        //7
        "What... Who are you?",
        //8
        "Hello! My name is Zarwid.\n" +
            "Thank you for freeing me.",
        //9
        "Are you... a genie?",
        //10
        "No! I'm a wizard!\n",
        //11
        "Too bad, I would have liked to have a wish.",
        //12
        "Oh, you know, we can always make a deal!\n" +
            "What's your dearest wish?",
        //13
        "I'd like to cross the mountains and travel the world!",
        //14
        "Well, may your wish come true!",
        //15
        "ABRACADABRA!",
        //16
        "Er...",
        //17
        "He... disappeared?",
        //18
        "What about my wish?",
        //19
        "Well, let’s go outside and see if anything has changed.",

        //20
        "The mountains are on the right.\n" +
            "Let's check them!",

        //21
        "Hmm, this is strange...",

        //22
        "It feels like the mountains are much smaller than usual.",
        //23
        "Hey, wait... I’m the one who’s giant!\n" +
            "What the heck?",
        //24
        "Well! You did want to cross the mountains, didn’t you?",

        //25
        "Isn't that a bit excessive?!",

        //26
        "All I did was make your wish come true.\n" +
            "Go on, try to push a mountain!",

        //27
        "What? Can I do that? But this is terrible! I'll totally destroy the landscape!",

        //28
        "Don't worry! Nature is stronger than you think!",

        //29
        "But what if... What if I want to get back home? " +
            "Won’t I crush my house if I return?",

        //30
        "Well, no, actually. Because the spell I cast on you lets you return to your normal size every time you pass through a human-sized opening." +
            "In this case, your fence.",

        //31
        "This spell is really weird...",

        //32
        "Come on, give it a try! You’ll see, it’s actually pretty fun!",
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
        //3
        "Bob",
        //4
        "Zoé",
    };

    public static int[] nameText =
    {
        0, //0
        0,
        0,
        0,
        0,
       
        0, //5
        0, 
        1, //7
        2,
        1,
        
        2 ,//10
        1,
        2,
        1,
        2,
        
        0, //15
        1,
        0,
        0,
        0,
        
        1, //20
        1,
        0,
        0,
        2,

        1, //25
        2,
        1,
        2,
        1,

        2, //30
        1,
        2,
        3, //PNJ 33
        4,
        0,
    };

    public static Vector2Int[] dialogs =
    {
        //0
        new Vector2Int(7, 15),

        //1
        new Vector2Int(16, 19),

        //2
        new Vector2Int(20, 20),

        //3
        new Vector2Int(21, 28),

        //4
        new Vector2Int(29, 29),

        //5
        new Vector2Int(30, 32),

        //6
        new Vector2Int(33, 33),

        //7
        new Vector2Int(34, 35),
    };

    public static Vector2Int getDialog(int i)
    {
        if(i >=0 && i < dialogs.Length)
        {
            return dialogs[i];
        }
        Debug.Log("Out of reach");
        return new Vector2Int(0,0);
    }

    public static string getName(int i)
    {
        //Debug.Log(i + " : " + nameText[i]);
        if (nameText[i] != 0)
        {
            //Debug.Log("name : "+ actualName);
            actualName = names[nameText[i]];
            //Debug.Log("new name : " + actualName);
        }

        return actualName;
    }
}
