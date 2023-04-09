namespace _GAME_.Scripts._SYSTEMS_._Character_System_.Interface
{
	public interface IState
	{
		void OnTick();
		void OnEnter();
		void OnExit();
	}
}