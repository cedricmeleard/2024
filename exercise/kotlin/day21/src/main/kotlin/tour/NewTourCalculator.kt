package tour

import arrow.core.Either
import arrow.core.left
import arrow.core.right
import java.time.Duration
import java.time.LocalTime

data class Step(val time: LocalTime, val label: String, val deliveryTime: Int)

class NewTourCalculator(private var steps: List<Step>) {
    private var calculated: Boolean = false
    private var totalDeliveryTime: Double = 0.0
    private val sortedSteps = steps.sortedBy { it.time }

    fun calculate(): Either<String, String> {
        if (steps.isEmpty()) {
            return "No locations !!!".left()
        }

        val result = StringBuilder()

        if (!calculated) {

            sortedSteps.forEach { step ->
                this.totalDeliveryTime += step.deliveryTime
                result.appendLine(fLine(step))
            }

        }

        val str: String = formatDurationToHHMMSS(totalDeliveryTime)
        result.appendLine("Delivery time | $str")
        calculated = true

        return result.toString().right()
    }

    private fun formatDurationToHHMMSS(deliveryTime: Double): String {
        val duration = Duration.ofSeconds(
            deliveryTime.toLong()
        )
        return "${duration.toHours()}%02d:${duration.toMinutesPart()}%02d:${duration.toSecondsPart()}%02d"
    }


    private fun fLine(step: Step?): String {
        if (step == null) {
            throw IllegalStateException()
        }

        return step.let {
            "${it.time} : ${it.label} | ${it.deliveryTime} sec"
        }
    }

}
