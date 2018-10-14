using System;
using System.Collections.Generic;
using System.IO;
using System.XML.Serialization;
using Demo_FileIO_NTier.Models;
using Newtonsoft;
using Demo_FileIO_NTier.DataAccessLayer;
using Newtonsoft.Json;

namespace Demo_FileIO_NTier.DataAccessLayer
{
    public class JsonDataService : IDataService
    {
        private string _dataFilePath;

        public IEnumerable<Character> ReadAll()
        {
            List<Character> characters = new List<Character>();

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    characters characterList = JsonConvert.DeserializeObject<RootObject>(jsonString).Characters;

                    characters = characterList.Character;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return characters;
        }

        public void WriteAll(IEnumerable<Character> characters)
        {
            RootObject rootObject = new RootObject();
            rootObject.Characters = new Characters();
            rootObject.Characters.Character = characters as List<Character>;

            string jsonString = JsonConvert.SerializeObject(rootObject);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public JsonDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }
    }
}