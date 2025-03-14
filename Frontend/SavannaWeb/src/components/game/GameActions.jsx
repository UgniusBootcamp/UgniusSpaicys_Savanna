import PropTypes from 'prop-types';
import SignalRService from '../../api/SignalRService';

const GameActions = ({ AnimalTypes }) => {
  const handleAnimalAdd = async (animalTypeId) => {
    await SignalRService.invokeCreateAnimal(animalTypeId);
  };

  return (
    <div className="justify-center items-center w-full text-primary-800 p-2">
      <h1 className="text-center mb-2 font-bold">Add Animal</h1>
      <div className="flex flex-wrap gap-2 justify-center">
        {AnimalTypes.map((animalType) => (
          <div
            className="text-white px-4 py-2 cursor-pointer bg-primary-900 hover:bg-amber-800 rounded-md transition duration-300 hover:-translate-y-1"
            key={animalType.id}
            onClick={() => handleAnimalAdd(animalType.id)}
          >
            {animalType.animalType}
          </div>
        ))}
      </div>
    </div>
  );
};

GameActions.propTypes = {
  AnimalTypes: PropTypes.array.isRequired,
};

export default GameActions;
