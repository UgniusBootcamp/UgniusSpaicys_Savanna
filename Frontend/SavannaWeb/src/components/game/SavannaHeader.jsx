import PropTypes from 'prop-types';
import { useEffect, useState } from 'react';
import GameApi from '../../api/GameApi';
import switchConstants from '../../constants/switchConstants';
import { Spinner } from '../common/Spinner';
import ToggleSwitch from '../common/ToggleSwitch';
import GameActions from './GameActions';
import GameStats from './GameStats';

const SavannaHeader = ({ game, isIconOn, onCheck }) => {
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
};

export default SavannaHeader;
