export interface IActionLink {
  text: string;
  isUpperCase?: boolean;
}

const ActionLink: React.FC<IActionLink> = ({ text, isUpperCase }) => {
  return (
    <div className={`text-blue-600 hover:text-blue-300 ${isUpperCase && 'uppercase'}`}>
      {text}
    </div>
  );
};

export default ActionLink;
