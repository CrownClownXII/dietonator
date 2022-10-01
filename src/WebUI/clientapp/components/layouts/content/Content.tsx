export interface IContent {
  children: JSX.Element;
}

const Content = ({ children }: IContent): JSX.Element => {
  return <div className="px-2.5 py-10">{children}</div>;
};

export default Content;
