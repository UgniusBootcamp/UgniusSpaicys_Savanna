import PropTypes from 'prop-types';
import { useEffect, useState } from 'react';
import GameApi from '../../api/GameApi';
import switchConstants from '../../constants/switchConstants';
import { Spinner } from '../common/Spinner';
import ToggleSwitch from '../common/ToggleSwitch';
import GameActions from './GameActions';
import GameStats from './GameStats';
import PauseResumeButton from './PauseResumeButton';
import QuitGameButton from './QuitGameButton';
import SaveGameButton from './SaveGameButton';

const SavannaHeader = ({ game, isIconOn, onCheck, onQuit }) => {
  const [isLoading, setIsLoading] = useState(true);
  const [AnimalTypes, setAnimalTypes] = useState(null);

  useEffect(() => {
    const loadAnimalTypes = async () => {
      setIsLoading(true);
      const response = await GameApi.getAnimalTypes();
      setAnimalTypes(response.data);
      setIsLoading(false);
    };

    loadAnimalTypes();
  }, []);

  if (isLoading) return <Spinner />;

  return (
    <div className="rounded-md bg-primary-200  w-full flex justify-between items-center gap-2">
      <GameStats
        AnimalTypes={AnimalTypes}
        iteration={game.iteration}
        animalsCount={game.animalCount}
      />
      <GameActions AnimalTypes={AnimalTypes} />
      <div className="flex flex-col gap-2 py-2">
        <PauseResumeButton isRunning={game.isRunning} />
        {!game.isRunning && <SaveGameButton />}
        {!game.isRunning && <QuitGameButton onQuit={onQuit} />}
      </div>
      <ToggleSwitch
        label={switchConstants.label}
        checked={isIconOn}
        onChange={(e) => onCheck(e.target.checked)}
      />
    </div>
  );
};

SavannaHeader.propTypes = {
  game: PropTypes.object.isRequired,
  isIconOn: PropTypes.bool.isRequired,
  onCheck: PropTypes.func.isRequired,
  onQuit: PropTypes.func.isRequired,
};

export default SavannaHeader;
