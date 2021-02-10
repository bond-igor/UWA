import React, { Component } from 'react';
import FileUpload from './FileUpload';
import axios from 'axios';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { data: [] };
  }

  componentDidMount() {
    this.populateData();
  }

  populateData = () => {
    axios.get('api/photo').then((res) => {
      console.log(res);
      this.setState({ data: res.data });
    });
  };

  render() {
    return (
      <div>
        <FileUpload></FileUpload>
        <h4>Files:</h4>
        <ul>
          {this.state.data.map((data) => (
            <li key={data.id}>{data.ref}</li>
          ))}
        </ul>
        <div>
          <button onClick={this.populateData}>Refresh data</button>
        </div>
      </div>
    );
  }
}
