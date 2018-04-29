using Cobble.Core.Lib;
using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.SpyHunter.Managers {
    [DisallowMultipleComponent]
    public class LevelManager : Manager<LevelManager> {
        
        public Texture2D[] TerrainTextures;

        public Terrain TerrainObject;

        private static int _lastTextureIndex;

        private void Start() {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode) {
            GameManager.Instance.UnpauseGame();
            if (!TerrainObject)
                TerrainObject = FindObjectOfType<Terrain>();
            ChangeTerrainTexture(0);
        }
        
        private void OnApplicationQuit() {
            ChangeTerrainTexture(0);
        }

        public void OnRegionTransition() {
            ChangeRandomTerrainTexture();
        }
        
        private void ChangeRandomTerrainTexture() {
            int textureIndex;
            do {
                textureIndex = Random.Range(0, TerrainTextures.Length);
            } while (textureIndex == _lastTextureIndex);
            ChangeTerrainTexture(textureIndex);
        }

        private void ChangeTerrainTexture(int textureIndex) {
            if (!TerrainObject) return;
            var splatPrototypes = TerrainObject.terrainData.splatPrototypes;
            if (splatPrototypes[0] == null) return;
            splatPrototypes[0].texture = TerrainTextures[textureIndex];
            TerrainObject.terrainData.splatPrototypes = splatPrototypes;
            _lastTextureIndex = textureIndex;
        }
    }
}