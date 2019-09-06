import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';


class Training extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            trainingCategory: [
                { id: 1, name: 'Yazılım', },
            ],
            training: [
                {
                    trainingSeriesId: '',
                    trainingCategoryId: '',
                    name: '',
                    description: '',
                    beginDateTime: '',
                    endDateTime: ''
                }
            ],

        }
    }

    render() {
        const { training, trainingCategory } = this.state;
        const { classes, trainingSeriesId, trainingError } = this.props;

        this.state.training.map((training, id) => {
            training.trainingSeriesId = trainingSeriesId;
        })

        return (

            <div className={classes.root}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        {training.map((training, id) => (
                            <Grid item xs={12} sm={12} key={id}>
                                <Input
                                    error={trainingError}
                                    labelName={translate('TrainingName')}
                                    inputType="text"
                                    onChange={e => {
                                        training.name = e.target.value;
                                        this.props.basicTrainingModel(this.state.training);
                                    }}
                                />
                                <DropDown
                                    error={trainingError}
                                    labelName={translate('TrainingCategory')}
                                    data={trainingCategory}
                                    onChange={e => {
                                        training.trainingCategoryId = e.target.value;
                                        this.props.basicTrainingModel(this.state.training);
                                    }}
                                />
                                <Input
                                    error={trainingError}
                                    labelName={translate('BeginDate')}
                                    inputType="date"
                                    onChange={e => {
                                        training.beginDateTime = e.target.value;
                                        this.props.basicTrainingModel(this.state.training);
                                    }}
                                />
                                <Input
                                    error={trainingError}
                                    labelName={translate('EndDate')}
                                    inputType="date"
                                    onChange={e => {
                                        training.endDateTime = e.target.value;
                                        this.props.basicTrainingModel(this.state.training);
                                    }}
                                />
                                <Input
                                    error={trainingError}
                                    multiline
                                    labelName={translate('Description')}
                                    inputType="text"
                                    onChange={e => {
                                        training.description = e.target.value;
                                        this.props.basicTrainingModel(this.state.training);
                                    }}
                                />
                            </Grid>
                        ))}
                    </Grid>
                </Grid>
            </div>
        );
    }
}


export default withStyles(Style)(Training);