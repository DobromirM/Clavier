import React, { Component } from 'react';

export class Line extends Component {

    constructor(props) {
        super(props);
        this.state = {
            selected: "not-selected",
            updateHighlights: false
        }
    }

    componentDidUpdate()
    {
        if (this.props.highlightAction && this.state.updateHighlights)
        {
            this.setState({ updateHighlights: false });
            this.props.highlightAction();
        }
        
    }

    render() {

        if (this.props.highlightable) {
            return (
                <React.Fragment>
                    <div className={this.props.name + "-line highlightable"} id={this.props.name + "-line-" + this.props.id}>{this.props.id}<div className={this.state.selected + "-line"} lineid={this.props.id}></div></div>
                    </React.Fragment>
                );
        }
        else {
            return (<div className={this.props.name + "-line"} id={this.props.name + "-line-" + this.props.id}>{this.props.id}</div>);
        }
    }
}