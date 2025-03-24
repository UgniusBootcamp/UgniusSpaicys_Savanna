import PropTypes from 'prop-types';
import { useState } from 'react';

const FormSwitcher = ({ forms }) => {
  const [selectedForm, setSelectedForm] = useState(forms[0].label);

  return (
    <div>
      <div className="flex mb-6 w-full flex-col lg:flex-row gap-y-2 ">
        {forms.map((form, index) => (
          <button
            key={form.label}
            className={`hover:cursor-pointer flex-1 px-4 py-2 font-medium transition-colors duration-300 rounded-lg ${
              index === 0
                ? 'lg:rounded-l-lg lg:rounded-r-none'
                : index === forms.length - 1
                ? 'lg:rounded-r-lg lg:rounded-l-none'
                : 'lg:rounded-none'
            } ${
              selectedForm === form.label
                ? 'bg-primary-700 text-white'
                : 'bg-gray-200 text-gray-700'
            }`}
            onClick={() => setSelectedForm(form.label)}
          >
            {form.label}
          </button>
        ))}
      </div>
      <div>{forms.find((form) => form.label === selectedForm)?.component}</div>
    </div>
  );
};

FormSwitcher.propTypes = {
  forms: PropTypes.arr,
};

FormSwitcher.propTypes = {
  forms: PropTypes.arrayOf(
    PropTypes.shape({
      label: PropTypes.string.isRequired,
      component: PropTypes.element.isRequired,
    }),
  ).isRequired,
};

export default FormSwitcher;
