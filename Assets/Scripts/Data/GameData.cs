using Data.Game;
using System.Threading.Tasks;
using UnityEngine;

namespace DoubleAgent.Data
{
    public sealed class GameData : GameDataBase<GameData, GameStates>
    {
        public static AudioSource GlobalMusicChannel;

        public override string SaveFolder => throw new System.NotImplementedException();
        public override string SaveExtension => throw new System.NotImplementedException();
        public override bool CompressSaveFile => throw new System.NotImplementedException();

        protected override Task<string> GetMetaData() => null;
        protected override Task<string> GetSaveData() => null;
        protected override Task<MetaData> LoadMetaDataFromPath<MetaData>(string path, bool compressed) => null;
        protected override void SetGameData<SaveData>(SaveData data) { }

        public static int Score;

        protected override void Reset()
        {
            Score = 0;
        }
    }
}