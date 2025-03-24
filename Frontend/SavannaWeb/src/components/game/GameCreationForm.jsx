import gameCreationConstants from '../../constants/gameCreationConstants';
import FormSwitcher from '../form/FormSwitcher';
import CreateGame from './CreateGame';
import LoadGame from './LoadGame';

const GameCreationForm = () => {
  return (
    <div className="w-1/2 flex flex-col min-h-[80vh] ">
      <FormSwitcher
        forms={[
          {
            label: gameCreationConstants.createGame,
            component: <CreateGame />,
          },
          { label: gameCreationConstants.loadGame, component: <LoadGame /> },
        ]}
      />
    </div>
  );
};

export default GameCreationForm;
