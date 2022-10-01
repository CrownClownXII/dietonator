export interface ISeparator {}

const Separator = ({}: ISeparator): JSX.Element => {
  return (
    <div className="pt-4 mt-4 space-y-2 border-t border-gray-200 dark:border-gray-700"></div>
  );
};

export default Separator;
