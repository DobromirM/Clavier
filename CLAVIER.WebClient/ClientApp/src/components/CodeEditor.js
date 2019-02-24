import React, {Component} from 'react';
import App from '../App';
import AceEditor from 'react-ace';

import 'brace/mode/csharp';
import 'brace/theme/xcode';

export class CodeEditor extends Component {

    static driverCode = "";

    static updateCode(code) {
        code = code.split("\n");
        App.connection.invoke("UpdateCode", code).catch(err => console.error(err.toString()));
    }

    static highlight = function(e)
    {
        if (e.target && e.target.classList[0] == 'ace_gutter-cell') {
            let lineNumber = parseInt(e.target.innerHTML);
            App.connection.invoke("UpdateCodeHighlights", lineNumber).catch(err => console.error(err.toString()));
        }
    };
    
    componentWillUpdate()
    {
        if (this.props.role == "navigator") {

            document.addEventListener('click', CodeEditor.highlight)
        }

        if (this.props.role == "driver") {

            document.removeEventListener('click', CodeEditor.highlight);
        }
    }

    static renderCode(code) {

        if (code) {
            return code.join('\n');
        }
        else {
            return "";
        }
    }

    createMarkers(lines) {

        let markers = [];

        for (let l in lines) {

            let marker = { startRow: lines[l] - 1, endRow: lines[l], type: "background", className: "yellow-marker" };
            markers.push(marker)
        }

        return markers;
    }

    onChange(newValue) {
        CodeEditor.updateCode(newValue);
        CodeEditor.driverCode = newValue;
    }

    render() {

        let markers = this.createMarkers(this.props.highlightedLines);

        return (
            this.props.role === "driver" ?
                <AceEditor mode="csharp" markers={markers} theme="xcode" name="code-editor" value={CodeEditor.driverCode}
                           onChange={this.onChange} fontSize={16} editorProps={{$blockScrolling: true}}/> :
                <AceEditor mode="csharp" markers={markers} theme="xcode" name="code-editor" value={CodeEditor.renderCode(this.props.code)}
                           fontSize={16} readOnly={true} editorProps={{$blockScrolling: true}}/>
        );
    }
}
