import PropTypes from 'prop-types';
import { useEffect, useRef, useState } from 'react';

const Game = ({ selected, map, onSelect }) => {
  const { width, height, animals } = map;
  const containerRef = useRef(null);
  const [cellHeight, setCellHeight] = useState(32);
  const [cellWidth, setCellWidth] = useState(32);

  useEffect(() => {
    const handleResize = () => {
      if (containerRef.current) {
        const { width: containerWidth, height: containerHeight } =
          containerRef.current.getBoundingClientRect();

        const maxCellWidth = Math.floor(containerWidth / width);
        const maxCellHeight = Math.floor(containerHeight / height);

        setCellHeight(maxCellHeight);
        setCellWidth(maxCellWidth);
      }
    };

    handleResize();
    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, [width, height]);

  const handleSelect = (newSelected) => {
    if (!newSelected) return;
    onSelect(newSelected);
  };

  const renderCells = () => {
    const cells = [];

    for (let y = 0; y < height; y++) {
      for (let x = 0; x < width; x++) {
        const animal = animals.find((a) => a.x === x && a.y === y);
        cells.push(
          <div
            onClick={animal ? () => handleSelect(animal) : null}
            key={`${x}-${y}`}
            className={`flex items-center justify-center font-semibold ${
              animal ? 'bg-primary-400 rounded-full cursor-pointer' : ''
            } ${
              animal && animal.id === selected?.id
                ? 'border-2 text-primary-900'
                : ''
            } `}
            style={{ width: `${cellWidth}px`, height: `${cellHeight}px` }}
          >
            {animal ? animal.name : ''}
          </div>,
        );
      }
    }

    return cells;
  };

  return (
    <div
      ref={containerRef}
      className="w-4/5 h-full border-4 border-primary-600 bg-primary-200/75 rounded-md overflow-hidden"
    >
      <div
        className="grid justify-center items-center"
        style={{
          gridTemplateColumns: `repeat(${width}, ${cellWidth}px)`,
          gridTemplateRows: `repeat(${height}, ${cellHeight}px)`,
        }}
      >
        {renderCells()}
      </div>
    </div>
  );
};

Game.propTypes = {
  selected: PropTypes.object,
  map: PropTypes.object.isRequired,
  onSelect: PropTypes.func.isRequired,
};

export default Game;
