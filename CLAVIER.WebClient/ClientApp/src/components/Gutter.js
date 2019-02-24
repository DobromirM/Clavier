import React, { Component } from 'react';
import { Line } from './Line';

export class Gutter extends Component {

    render() {

        let editorName = this.props.name;
        let lines = [];

        for (var i = 1; i <= this.props.lineCount; i++) {
            lines.push(<Line name={editorName} highlightAction={this.props.highlightAction} highlightable={this.props.highlightable} id={i} key={"line-" + i}/>);
        }

        return (
            <div className={editorName}>
                {lines}
            </div>
        );
    }
}
