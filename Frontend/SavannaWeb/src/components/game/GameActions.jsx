import { useEffect, useState } from 'react';
import GameApi from '../../api/GameApi';
import SignalRService from '../../api/SignalRService';
import { Spinner } from '../common/Spinner';

const GameActions = () => {
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

  const handleAnimalAdd = async (animalTypeId) => {
    await SignalRService.invokeCreateAnimal(animalTypeId);
  };

  return (
    <div className="bg-primary-400 rounded-md justify-center items-center">
      <div className="p-4">
        <h1 className="text-center mb-2">Add Animal</h1>
        <div className="flex flex-wrap gap-2 justify-center">
          {AnimalTypes.map((animalType) => (
            <div
              className="px-4 py-2 cursor-pointer bg-primary-900 hover:bg-amber-800 rounded-md transition duration-300 hover:-translate-y-1"
              key={animalType.id}
              onClick={() => handleAnimalAdd(animalType.id)}
            >
              {animalType.animalType}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default GameActions;
