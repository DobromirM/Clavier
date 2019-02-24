import React, { Component } from 'react';
import { Gutter } from './Gutter';
import App from '../App';
import { NotesButton } from './NotesButton';
import * as $ from 'jquery';

export class NoteEditor extends Component {

    shouldComponentUpdate() {
        return NotesButton.unfrozen;
    }

    componentDidUpdate() {
        $("#note-editor").scroll(function () {
            $(".note-gutter").scrollTop($("#note-editor").scrollTop());
        });
    }

    updateNotes() {
        let notes = document.getElementById("note-editor").value.split("\n");
        App.connection.invoke("UpdateNotes", notes).catch(err => console.error(err.toString()));
    }

    renderNotes(noteEditor) {
   
        let editor = document.getElementById("note-editor");

        if (editor && noteEditor.props.notes) {
            editor.value = noteEditor.props.notes.join('\n');
            }

    }

    countLines(noteEditor) {
        if (noteEditor.props.notes) {
            return noteEditor.props.notes.length;
        }
        else {
            return 1;
        }
    }

    render() {

        return (
            <React.Fragment>
                <Gutter name="note-gutter" highlightable={false} lineCount={this.countLines(this)} />
                {this.props.role === "driver" ?
                    <textarea id="note-editor" readOnly>{this.renderNotes(this)}</textarea> :
                    <textarea id="note-editor" onChange={this.updateNotes}></textarea>
                }
            </React.Fragment>
        );

    }
}
