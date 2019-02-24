import React, { Component } from 'react';
import App from '../App';
import { CodeEditor } from './CodeEditor';

export class OpenButton extends Component {

	constructor(props) {
        super(props);
		this.fileReader = new FileReader();
		this.displayFile = this.displayFile.bind(this);
		this.readFile = this.readFile.bind(this);
    }
	
	render() {
        return (
            <React.Fragment>
                <button className="open-file-button" id="open-file-button" onClick={() => this.rerouteClick(this)}>Open file</button>
                <input type="file" id="open-file-input" value="" ref="openFileInput" onChange={e => this.readFile(e.target.files[0])} style={{ display: "none" }} />
            </React.Fragment>
		);
    }
	
	rerouteClick(e) {
		this.refs.openFileInput.click();
	}

	readFile(file) {
		this.fileReader.onloadend = this.displayFile;
		this.fileReader.readAsText(file);
	}
	
	displayFile(e) {
		const content = this.fileReader.result;
		var lines = content.split("\n");
        App.connection.invoke("UpdateCode", lines).catch(err => console.error(err.toString()));
		CodeEditor.driverCode = content;
	}
}