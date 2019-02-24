import React, { Component } from 'react';

export class NotesButton extends Component {

    static unfrozen = true;

    toggleStatus(button)
    {
        NotesButton.unfrozen = !NotesButton.unfrozen;

        if (NotesButton.unfrozen) {
            button.setState({command: "Freeze"})
        }
        else {
            button.setState({command: "Unfreeze"})
        }

        button.props.render();
    }

    constructor(props) {
        super(props);

        this.state = {
            command: "Freeze"
        }
    }

    render() {

        return (
            <button className="freeze-button button" id={this.state.command} type="button" onClick={() => this.toggleStatus(this)}>{this.state.command + " Notes"}</button>
               );
    }
}