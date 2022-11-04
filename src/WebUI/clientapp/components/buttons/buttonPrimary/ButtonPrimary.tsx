export interface IButtonPrimary {
  text: string;
  onClick: () => void;
}

const ButtonPrimary = ({ text, onClick }: IButtonPrimary) => {
  return (
    <button
      className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
      onClick={onClick}
    >
      {text}
    </button>
  );
};

export default ButtonPrimary;
