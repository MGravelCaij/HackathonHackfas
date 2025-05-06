using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonC
{
    public class Answer
    {

        public string sentence { get; set; }
        public string explanation { get; set; }
        public string confiance { get; set; }
        public string intensity { get; set; }
        public string GetString()
        {
            return $"Phrase {sentence}\n Explication {explanation}\n confiance {confiance} \n Intensité  {intensity}\n";
        }
        public Answer() { }


        public static ChatCompletionOptions GetOptions()
        {
            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
        jsonSchemaFormatName: "logical_reasoning",
        jsonSchema: BinaryData.FromBytes("""
            {
                "type": "object",
                "properties": {
                "answers": {
                    "type": "array",
                    "items": {
                    "type": "object",
                    "properties": {
                        "sentence": { "type": "string" },
                        "explanation": { "type": "string" },
                        "confiance": { "type": "number" },
                        "intensite": { "type": "number" },
                        "output": { "type": "string" }
                    },
                    "required": ["explanation", "output","confiance","intensite","sentence"],
                    "additionalProperties": false
                    }
                }
                },
                "required": ["answers"],
                "additionalProperties": false
            }
            """u8.ToArray()),
        jsonSchemaIsStrict: true)
            };
            return options;
        }

    }
}
