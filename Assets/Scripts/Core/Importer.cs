using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Models;
using Net.PackageData;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace Core
{
    public static class Importer
    {
        public static Asteroids ImportAsteroids(string filepath)
        {
            var file = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<Asteroids>(file);
        }

        public static void ExportAsteroids(string filepath = "./asteroids.json")
        {
            var asteroids = GameObject.FindGameObjectsWithTag(Constants.AsteroidTag)
                .Select(obj => new WorldObject(obj.name, obj.transform)).ToList();
            var textToWrite = JsonConvert.SerializeObject(asteroids);
            File.WriteAllText(filepath, textToWrite);
        }

        public static void ExportAsteroids(Asteroids asteroids, string filepath = "./asteroids.json")
        {
            var textToWrite = JsonConvert.SerializeObject(asteroids);
            File.WriteAllText(filepath, textToWrite);
        }

        public static IEnumerator AddAsteroidsOnScene(Asteroids asteroids)
        {
            foreach (var asteroid in asteroids.asteroids)
            {
                InstantiateHelper.InstantiateObject(asteroid);
                yield return null;
            }
        }
    }
}