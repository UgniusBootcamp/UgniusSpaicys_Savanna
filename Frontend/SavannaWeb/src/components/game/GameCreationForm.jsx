import { useState } from 'react';
import SignalRService from '../../api/SignalRService';
import Button from '../common/Button';

const GameCreationForm = () => {
  const [formData, setFormData] = useState({
    Height: 20,
    Width: 20,
  });

  const handleChanges = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const gameCreateDto = {
      Height: parseInt(formData.Height),
      Width: parseInt(formData.Width),
    };

    SignalRService.invokeCreateGame(gameCreateDto);
  };

  return (
    <div className="w-full flex justify-center items-center">
      <div className="w-full sm:w-96 rounded-lg bg-primary-300 p-6 flex flex-col items-center">
        <form onSubmit={handleSubmit}>
          <h2 className="text-2xl font-semibold text-primary-900 mb-6">
            Create New Game
          </h2>
          <div className="w-full mb-4">
            <label
              htmlFor="Height"
              className="block text-sm font-medium text-primary-950"
            >
              Height
            </label>
            <input
              id="Height"
              name="Height"
              onChange={handleChanges}
              value={formData.Height}
              type="number"
              min={5}
              max={40}
              required
              className="mt-1 p-2 w-full bg-primary-100 border border-primary-200 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-100 focus:border-primary-100 transition duration-200"
              placeholder={'Enter Height'}
            />
          </div>

          <div className="w-full mb-6">
            <label
              htmlFor="Width"
              className="block text-sm font-medium text-primary-950"
            >
              Width
            </label>
            <input
              id="Width"
              name="Width"
              onChange={handleChanges}
              value={formData.Width}
              type="number"
              required
              className="mt-1 p-2 w-full bg-primary-100 border border-primary-200 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-100 focus:border-primary-100 transition duration-200"
              placeholder={'Enter Width'}
            />
          </div>
          <Button className="w-full bg-primary-900 hover:bg-primary-700 mb-4">
            {'Start Game'}
          </Button>
        </form>
      </div>
    </div>
  );
};

export default GameCreationForm;
