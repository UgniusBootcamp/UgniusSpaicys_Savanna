import PropTypes from 'prop-types';

const FormTemplate = ({ children, header }) => {
  return (
    <div className="w-full flex justify-center items-center">
      <div className="w-full sm:w-96 border-2 rounded-lg bg-primary-50 border-primary-300 p-6 flex flex-col items-center">
        <h2 className="text-2xl font-semibold text-gray-800 mb-6">{header}</h2>
        <div className="w-full mb-4">{children}</div>
      </div>
    </div>
  );
};

export default FormTemplate;

FormTemplate.propTypes = {
  children: PropTypes.node.isRequired,
  header: PropTypes.string,
};
