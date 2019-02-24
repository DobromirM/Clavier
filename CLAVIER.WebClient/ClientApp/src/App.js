import React, {Component} from 'react';
import {CodeEditor} from './components/CodeEditor';
import {NoteEditor} from './components/NoteEditor';
import {Controls} from './components/Controls';
import {Error} from './components/Error';
import { Loading } from './components/Loading';
import { OpenButton } from './components/OpenButton';
import * as signalR from '@aspnet/signalr';

export default class App extends Component {

    static url = new URL(window.location.href);
    static group = App.getGroup();
    static host = new URL(window.location.href).hostname;
    
    static connection = new signalR.HubConnectionBuilder().withUrl("https://" + App.host + ":44327/editorhub?group=" + App.group).configureLogging(signalR.LogLevel.Information).build();

    constructor(props) {
        super(props);

        this.forceRender = this.forceRender.bind(this);

        this.state = {
            role: null,
            code: "",
            notes: null,
            highlightedLines: [],
            error: null,
            switch: false
        };
    }

    static getGroup() {
        
        if (App.url.searchParams) {
            return App.url.searchParams.get("group");
        }
        else {
            return "";
        }
    }

    forceRender() {
        this.forceUpdate();
    }

    download(something) {
        window.open('https://' + App.host + ':44327/file/download/' + App.group);
    }
    
    componentDidMount() {

        App.connection.start().catch(err => console.error(err.toString()));

        App.connection.on("ReceiveCodeUpdate", (message) => {
            this.setState({code: message});
        });

        App.connection.on("ReceiveCodeHighlightsUpdate", (message) => {
            if (this.state.highlightedLines.includes(message)) {
                let index = this.state.highlightedLines.indexOf(message);
                this.state.highlightedLines.splice(index, 1);
            }
            else {
                this.state.highlightedLines.push(message);
            }

            this.forceRender();
        });

        App.connection.on("ReceiveNotesUpdate", (message) => {
            this.setState({notes: message});
        });

        App.connection.on("ReceiveError", () => {
            this.setState({error: true});
        });

        App.connection.on("ReceiveGroup", (message) => {
            window.history.pushState("", "", "/join?group=" + message);
            App.group = message;
        });

        App.connection.on("ReceiveSwitchUpdate", (message) => {
            this.setState({switch: message});
        });

        App.connection.on("PartnerDisconnected", () => {
            this.state.highlightedLines = [];
            this.setState({ notes: [] });
        });

        App.connection.on("ReceiveRole", (message) => {

            if (message === 0) {
                this.setState({role: "driver"});
                CodeEditor.driverCode = CodeEditor.renderCode(this.state.code);
            }
            else {
                this.setState({role: "navigator"});
                this.setState({code: CodeEditor.driverCode.split("\n")});
            }
        });
    }

    render() {
        if (this.state.error) {
            return (<Error/>);
        }

        if (this.state.role) {
            return (
                <React.Fragment>
                    <div className="group">Group ID: {App.group}</div>
                    <div className="actions-wrapper">
                    </div>
                    <div className="code-editor-wrapper">
                        <CodeEditor code={this.state.code} highlightedLines={this.state.highlightedLines}
                                    role={this.state.role}/>
                    </div>

                    <button className="download" onClick={() => this.download(this)}>Download</button>
                    {this.state.role === "driver" ?
                        <OpenButton render={this.props.render} /> :
                        <div></div>
                    }

                    <div className="note-editor-wrapper">
                        <NoteEditor notes={this.state.notes} role={this.state.role}/>
                        <Controls render={this.forceRender} role={this.state.role} switch={this.state.switch}/>
                    </div>
                </React.Fragment>
            );
        }
        else {
            return (<Loading/>)
        }
    }
}
