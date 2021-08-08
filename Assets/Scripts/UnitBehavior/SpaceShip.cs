using Core;

namespace UnitBehavior { 

    public class SpaceShip : SpaceUnit
    {
        public UnitState state;

        protected override void Awake()
        {
            base.Awake();
            //initialize all components
            //Иногда лучше компоненты все прописатьв теле сущности и найти один раз ,чем каждый раз обращаться к ним через поиск.
            //Это быстрее
        }

        private void Start()
        {
            //get state
        }

        private void Update()
        {

        }

        private void MovementHandler()
        {

        }

        private void CollisionEnter()
        {

        }

    }
}