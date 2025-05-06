using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonC
{
    public class Prompt
    {
        public List<UserChatMessage> messages;
        public Prompt()
        {
          
        }

        public List<ChatMessage> CreatePrompt(string legalText)
        {
            List<String> exemples = ["Le tribunal ne peut considérer qu'il est une toute jeune personne."];
            List<string> contreExemples = ["Les actes de l'accusé ont causé un méfait à la victime."];
            List<string> termesPositifs = ["semble","bien entendu", "évidamment", "de toute évidence", "flagrant"];

            List<ChatMessage> msgs = [
            new UserChatMessage("Tu es un assistant juridique. Extrait les 5 phrases avec un haut degré de sujectivité dans leur écriture.\n"),
            new UserChatMessage(" La subjectivité d'un texte signifie le niveau d'assurance que les faits dictés sont justes dans le texte.\n."),
                            new UserChatMessage($"Voici un exemple d'une phrase subjective : {string.Join("\n",exemples)}."),
                            new UserChatMessage($"Voici un exemple d'une phrase objective :  {string.Join("\n",contreExemples)}."),
                 new UserChatMessage($"Voici une liste de termes que l'on considérons à caractère subjectif :  [{string.Join(",",contreExemples)}].\n"),
                new UserChatMessage("Il faut prendre en compte les caractéristiques des acteurs décrits dans le texte.\n La réponse doit suivre les déterminants suivants :"),
                new UserChatMessage("- La phrase subjective.\r\n"),
                new UserChatMessage("- Le degré de confiance.\r\n"),
                new UserChatMessage("- Le degré d'intensité subjective.\r\n"),
                new UserChatMessage("Voici le texte à analyser :\r\n"),
            ];
            foreach (var p in legalText.Split("[SEP]"))
            {
                msgs.Add(new UserChatMessage(p));
            }

            return msgs;
        }

    }
}
