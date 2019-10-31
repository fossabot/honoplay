import React, { Component } from 'react';
import DropzoneComponent from 'react-dropzone-component';
import 'dropzone/dist/dropzone.css';
import 'react-dropzone-component/styles/filepicker.css';

class InputDropzoneComponent extends Component {
  constructor(props) {
    super(props);
  }

  array = file => {
    if (!file) {
      return;
    }
    const reader = new FileReader();
    reader.readAsArrayBuffer(file);

    reader.onload = e => {
      const arrayBuffer = e.target.result,
        array = Array.from(new Uint8Array(arrayBuffer));
      this.props.selectedImage(array);
      this.setState({ byteArray: array });
    };
    const name = file.name;
    const type = file.type;
    this.props.name(name);
    this.props.type(type);
  };

  render() {
    const { imageData } = this.props;
    const componentConfig = {
      iconFiletypes: ['.jpg', '.png', '.gif'],
      showFiletypeIcon: true,
      postUrl: 'https://httpbin.org/post'
    };
    const djsConfig = {
      addRemoveLinks: true,
      acceptedFiles: 'image/jpeg,image/jpg,image/png',
      maxFiles: 1,
      dictDefaultMessage: 'new default message'
    };
    const eventHandlers = {
      addedfile: file => this.array(file)
      //init: imageData => `data:image/jpeg;base64,${imageData}`
    };
    console.log('deneme', imageData);
    return (
      <div className="dropzone-wrapper">
        <DropzoneComponent
          config={componentConfig}
          eventHandlers={eventHandlers}
          djsConfig={djsConfig}
        />
      </div>
    );
  }
}

export default InputDropzoneComponent;
