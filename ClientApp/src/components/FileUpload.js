import React, { Component } from 'react';
import axios from 'axios';

export class FileUpload extends Component {
  state = {
    selectedFile: null,
  };

  onFileChange = (event) => {
    this.setState({ selectedFile: event.target.files[0] });
  };

  async onFileUpload() {
    const formData = new FormData();

    formData.append(
      'image',
      this.state.selectedFile,
      this.state.selectedFile.name
    );

    axios
      .put('api/photo', formData)
      .then((ref) => alert('Done')) // TODO - refresh data and clear selected file
      .catch((error) => {
        alert('Error');
      });
  }

  fileData = () => {
    if (this.state.selectedFile) {
      return (
        <div>
          <div>
            <button onClick={this.onFileUpload.bind(this)}>Upload!</button>
          </div>
          <h2>File Details:</h2>

          <p>File Name: {this.state.selectedFile.name}</p>

          <p>File Type: {this.state.selectedFile.type}</p>

          <p>
            Last Modified:{' '}
            {this.state.selectedFile.lastModifiedDate.toDateString()}
          </p>
        </div>
      );
    } else {
      return (
        <div>
          <br />
          <h4>Choose before Pressing the Upload button</h4>
        </div>
      );
    }
  };

  render() {
    return (
      <div>
        <h3>File Upload:</h3>
        <input type="file" onChange={this.onFileChange} />
        {this.fileData()}
      </div>
    );
  }
}

export default FileUpload;
