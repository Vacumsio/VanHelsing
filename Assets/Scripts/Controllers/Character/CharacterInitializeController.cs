﻿using UnityEngine;
using RootMotion.Dynamics;


namespace BeastHunter
{
    public sealed class CharacterInitializeController : IAwake
    {
        #region Field

        GameContext _context;

        #endregion


        #region ClassLifeCycle

        public CharacterInitializeController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            var characterData = Data.CharacterData;

            Vector3 instantiatePosition = characterData._characterCommonSettings.InstantiatePosition;
            Vector3 groundedInstancePosition = GetGroundedPosition(instantiatePosition);

            GameObject instance = GameObject.Instantiate(characterData._characterCommonSettings.Prefab);

            CharacterModel character = new CharacterModel(instance, characterData, groundedInstancePosition);
            _context.CharacterModel = character;

            //Transform ragdoll = GameObject.Instantiate(Data.PuppetPrefabData, groundedInstancePosition,
            //    Quaternion.identity) as Transform;
            //ragdoll.name = "Player";

            //// This will duplicate the ragdoll character, remove the ragdoll components from the original and use it as the animated target.
            //PuppetMaster.SetUp(ragdoll, 8, 9);

            //CharacterModel character = new CharacterModel(ragdoll.gameObject, characterData, groundedInstancePosition);
            //_context.CharacterModel = character;
        }

        #endregion


        #region Methods

        private Vector3 GetGroundedPosition(Vector3 startPosition)
        {
            Vector3 groundedPosition = new Vector3();

            bool isGroundBelow = Services.SharedInstance.PhysicsService.FindGround(startPosition, out groundedPosition);

            if (!isGroundBelow)
            {
                throw new System.Exception("Ground is above player's position!");
            }

            return groundedPosition;
        }

        #endregion
    }
}
