export interface IBaseTemplate {
  sampleTextProp: string;
}

const BaseTemplate = ({ sampleTextProp }: IBaseTemplate) => {
  return <div>{sampleTextProp}</div>;
};

export default BaseTemplate;
